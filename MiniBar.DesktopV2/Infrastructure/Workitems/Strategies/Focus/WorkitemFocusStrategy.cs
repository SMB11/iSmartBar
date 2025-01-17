﻿using Infrastructure.Framework;
using Infrastructure.Interface;
using Infrastructure.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Workitems.Strategies.Focus
{
    /// <summary>
    /// The focus strategy for a workitem
    /// Any workitem should be focused by calling 
    /// WorkitemFocusStrategy.Focus method
    /// </summary>
    internal abstract class WorkitemFocusStrategy
    {
        /// <summary>
        /// The Current ContextService
        /// </summary>
        protected ContextService CurrentContextService { get; private set; }

        /// <summary>
        /// The workitem to focus
        /// </summary>
        protected IWorkItem Workitem { get; private set; }

        /// <summary>
        /// Applciation logger
        /// </summary>
        protected ICompositeLogger Logger { get; private set; }

        /// <summary>
        /// Applciation task manager
        /// </summary>
        protected ITaskManager TaskManager { get; private set; }

        internal WorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            Logger = CommonServiceLocator.ServiceLocator.Current.GetInstance<ICompositeLogger>();
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
        }

        /// <summary>
        /// Get the startegy to focus the workitem
        /// The focus startegy should be obtained from 
        /// here and never be instaciated outside of this method
        /// </summary>
        /// <param name="contextService">ContextService</param>
        /// <param name="workItem">Workitem to open</param>
        public static WorkitemFocusStrategy GetFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            if (workItem == null || workItem is NullWorkitem)
                return new WorkitemUnfocusStrategy(currentContextService, workItem);
            else if (workItem.IsModal)
                return new ModalWorkitemFocusStrategy(currentContextService, workItem);
            else if (workItem.Parent != null)
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
            else
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
        }

        /// <summary>
        /// Focus the workitem
        /// </summary>
        public async Task Focus()
        {
            try
            {
                await Execute().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                // Log workitem exception
                Logger.LogWithWorkitemData("Failed to focus workitem", LogLevel.Exception, Workitem, e);
            }
        }

        protected abstract Task Execute();
    }
}
