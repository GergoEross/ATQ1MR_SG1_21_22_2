using ATQ1MR_HFT_2021221.Models.DTOs;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_SG1_21_22_2.WpfClient.BL.Interfaces;
using ATQ1MR_SG1_21_22_2.WpfClient.Infrasructure;
using ATQ1MR_SG1_21_22_2.WpfClient.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_SG1_21_22_2.WpfClient.BL.Implementation
{
    public class ProcessorHandlerService : IProcessorHandlerService
    {
        readonly IMessenger messenger;
        readonly IProcessorEditorService editorService;
        readonly IProcessorDisplayService displayService;
        HttpService processorHttpService;
        HttpService pBrandHttpService;

        public ProcessorHandlerService(IMessenger messenger, IProcessorEditorService editorService, IProcessorDisplayService displayService)
        {
            this.messenger = messenger;
            this.editorService = editorService;
            this.displayService = displayService;
            processorHttpService = new HttpService("Processor", "http://localhost:51252/api/");
            pBrandHttpService = new HttpService("PBrand", "http://localhost:51252/api/");
        }

        public void AddProcessor(IList<ProcessorModel> collection)
        {
            ProcessorModel processorToEdit = null;
            bool operationFinished = false;
            do
            {
                var newProcessor = editorService.EditProcessor(processorToEdit);

                if (newProcessor != null)
                {
                    var operationResult = processorHttpService.Create(new ProcessorDTO()
                    {
                        Socket = newProcessor.Socket,
                        Name = newProcessor.Name,
                        BaseClock = newProcessor.BaseClock,
                        BoostClock = newProcessor.BoostClock,
                        Cores = newProcessor.Cores,
                        Threads = newProcessor.Threads,
                        Price = newProcessor.Price,
                        BrandId = newProcessor.BrandId,
                        IsOvercolckable = newProcessor.IsOverclockable,
                        ReleaseDate = newProcessor.ReleaseDate
                    });

                    processorToEdit = newProcessor;
                    operationFinished = operationResult.IsSuccess;

                    if (operationResult.IsSuccess)
                    {
                        RefreshCollectionFromServer(collection);

                        SendMessage("Processor succefully added!");
                    }
                    else
                    {
                        SendMessage(operationResult.Messages.ToArray());
                    }
                }
                else
                {
                    SendMessage("Processor add cancelled");
                    operationFinished = true;
                }
            } while (!operationFinished);
        }

        private void SendMessage(params string[] messages)
        {
            messenger.Send(messages, "BLOperationResult");
        }

        private void RefreshCollectionFromServer(IList<ProcessorModel> collection)
        {
            collection.Clear();
            var newProcessors = GetAll();
            foreach (var processor in newProcessors)
            {
                collection.Add(processor);
            }
        }

        public void DeleteProcessor(IList<ProcessorModel> collection, ProcessorModel processor)
        {
            if (processor != null)
            {
                var operationResult = processorHttpService.Delete(processor.Id);

                if (operationResult.IsSuccess)
                {
                    RefreshCollectionFromServer(collection);
                    SendMessage("Processor deletion successful!");
                }
                else
                {
                    SendMessage(operationResult.Messages.ToArray());
                }
            }
            else
            {
                SendMessage("Must select an item!");
            }
        }


        public void ModifyProcessor(IList<ProcessorModel> collection, ProcessorModel processor)
        {
            ProcessorModel processorToEdit = processor;
            bool operationFinished = false;
            if (processor != null)
            {
                do
                {
                    var editedProcessor = editorService.EditProcessor(processorToEdit);

                    if (editedProcessor != null)
                    {
                        var operationResult = processorHttpService.Update(new ProcessorDTO()
                        {
                            Id = editedProcessor.Id,
                            Socket = editedProcessor.Socket,
                            Name = editedProcessor.Name,
                            BaseClock = editedProcessor.BaseClock,
                            BoostClock = editedProcessor.BoostClock,
                            Cores = editedProcessor.Cores,
                            Threads = editedProcessor.Threads,
                            Price = editedProcessor.Price,
                            BrandId = editedProcessor.BrandId,
                            IsOvercolckable = editedProcessor.IsOverclockable,
                            ReleaseDate = editedProcessor.ReleaseDate
                        });

                        processorToEdit = editedProcessor;
                        operationFinished = operationResult.IsSuccess;

                        if (operationResult.IsSuccess)
                        {
                            RefreshCollectionFromServer(collection);
                            SendMessage("Processor midification successful!");
                        }
                        else
                        {
                            SendMessage(operationResult.Messages.ToArray());
                        }
                    }
                    else
                    {
                        SendMessage("Processor modification cancelled!");
                        operationFinished = true;
                    }
                } while (!operationFinished);
            }
            else
            {
                SendMessage("Must select an item");
            }
        }

        public void ViewProcessor(ProcessorModel processor)
        {
            if (processor != null)
            {
                displayService.Display(processor);
            }
            else
            {
                SendMessage("Must select an item!");
            }
        }
        public IList<ProcessorModel> GetAll()
        {
            var processors = processorHttpService.GetAll<Processor>();

            return processors.Select(x => new ProcessorModel(x.Id, x.Socket, x.Name, x.BaseClock, x.BoostClock, x.Cores, x.Threads, x.Price, x.BrandId, x.IsOvercolckable, x.ReleaseDate)).ToList();
        }

        public IList<PBrandModel> GetAllBrands()
        {
            var pBrands = pBrandHttpService.GetAll<PBrand>();

            return pBrands.Select(x => new PBrandModel(x.Id, x.Name)).ToList();
        }
    }
}
