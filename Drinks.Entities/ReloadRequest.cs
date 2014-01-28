/*
 * This file is part of the Satimo Software Framework
 *
 * Copyright © Satimo 2014 All Rights Reserved, http://satimo.fr/
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class ReloadRequest
    {
        public ReloadRequest(decimal amount, int userId, int executorUserId)
        {
            if (amount <= 0)
                throw new ArgumentException("amount must be greater than 0");
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
