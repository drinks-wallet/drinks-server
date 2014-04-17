/*
 * This file is part of the Satimo Software Framework
 *
 * Copyright © Satimo 2014 All Rights Reserved, http://satimo.fr/
 */
using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class ReloadRequest
    {
        public ReloadRequest(decimal amount, int userId, int executorUserId)
        {
            Amount = amount;
            UserId = userId;
            ExecutorUserId = executorUserId;
        }

        [UsedImplicitly]
        private ReloadRequest() { }

        public decimal Amount { get; private set; }
        public int UserId { get; private set; }
        public int ExecutorUserId { get; private set; }
    }
}
