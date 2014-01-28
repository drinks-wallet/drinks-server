using System;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class Transaction
    {
        public Transaction(decimal amount, int userId, int executorUserId)
        {
            Amount = amount;
            UserId = userId;
            ExecutorUserId = executorUserId;
        }

        [UsedImplicitly]
        private Transaction() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }
        public decimal Amount { get; private set; }
        public int UserId { get; private set; }
        public int ExecutorUserId { get; private set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; private set; }
    }
}