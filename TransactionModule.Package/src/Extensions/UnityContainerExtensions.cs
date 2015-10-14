﻿using System;
using System.Linq;
using Microsoft.Practices.Unity;
using TransactionModule.Factories.FactoryMethods;
using TransactionModule.Factories.FactoryMethods.Interfaces;
using TransactionModule.Interfaces;
using TransactionModule.Repositories.Interfaces;
﻿using TransactionModule.Strategies.Interfaces;
﻿using TransactionModule.TransactionStateAppliers;
using TransactionModule.TransactionStateAppliers.Interfaces;
using TransactionModule.Validators;
using TransactionModule.Validators.Interfaces;

namespace TransactionModule.Extensions
{
    public static class UnityContainerExtensions
    {
        public static void RegisterTransactionHandler<TTransaction>(
            this IUnityContainer container, 
            Type transactionFactoryMethodType,
            Type transactionRepositoryType,
            Type transactionParticipantValidatorType,
            Type transactionSubjectValidatorType,
            Type operateWalletStrategyType,
            Type completeTransactionCallbackType) 
            where TTransaction : class, ITransaction
        {
            if (transactionFactoryMethodType.GetInterfaces().All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(ITransactionFactoryMethod<>))
                || transactionRepositoryType.GetInterfaces().All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(ITransactionRepository<>))
                || transactionParticipantValidatorType.GetInterfaces().All(x => x != typeof(ITransactionParticipantValidator<TTransaction>))
                || transactionSubjectValidatorType.GetInterfaces().All(x => x != typeof(ITransactionSubjectValidator<TTransaction>))
                || operateWalletStrategyType.GetInterfaces().All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(IOperateWalletStrategy<TTransaction>))
                || completeTransactionCallbackType.GetInterfaces().All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(ICompleteTransactionCallback<TTransaction>)))
            {
                throw new ArgumentException();
            }

            container.RegisterType(typeof (ITransactionFactoryMethod<>), transactionFactoryMethodType);
            container.RegisterType(typeof(ITransactionRepository<>), transactionRepositoryType);

            container.RegisterType(typeof(ITransactionParticipantValidator<TTransaction>), transactionParticipantValidatorType);
            container.RegisterType(typeof(ITransactionSubjectValidator<TTransaction>), transactionSubjectValidatorType);

            container.RegisterType(typeof(IOperateWalletStrategy<TTransaction>), operateWalletStrategyType);
            container.RegisterType(typeof(ICompleteTransactionCallback<TTransaction>), completeTransactionCallbackType);
        }

        public static void RegisterTransactionModule(this IUnityContainer container)
        {
            container.RegisterType(typeof(ITransactionModule<>), typeof(TransactionModule<>));

            container.RegisterType(typeof(IIndefiniteTransactionParticipantValidator), typeof(IndefiniteTransactionParticipantValidator<>));
            container.RegisterType(typeof(IAppTransactionParticipantValidator), typeof(AppTransactionParticipantValidator));

            container.RegisterType(typeof(IIndefiniteTransactionStateApplier<>), typeof(IndefiniteTransactionStateApplier<>));
            container.RegisterType(typeof(IProcessTransactionStateApplier<>), typeof(ProcessTransactionStateApplier<>));
            container.RegisterType(typeof(ICancelTransactionStateApplier<>), typeof(CancelTransactionStateApplier<>));
            container.RegisterType(typeof(IHoldTransactionStateApplier<>), typeof(HoldTransactionStateApplier<>));
            container.RegisterType(typeof(ICompleteTransactionStateApplier<>), typeof(CompleteTransactionStateApplier<>));

            container.RegisterType(typeof(ITransactionAmountValidator), typeof(TransactionAmountValidator));
        }
    }
}