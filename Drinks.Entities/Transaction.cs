using System;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class Transaction
    {
        public Transaction(decimal amount, int userId, int executorUserId)
        {
            Amount = Math.Round(amount, 2);
            UserId = userId;
            ExecutorUserId = executorUserId;
        }

        [UsedImplicitly]
        private Transaction() { }

        [UsedImplicitly]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [UsedImplicitly]
        public decimal Amount { get; private set; }

        [UsedImplicitly]
        public int UserId { get; private set; }

        [UsedImplicitly]
        public int ExecutorUserId { get; private set; }
        
        [UsedImplicitly]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; private set; }
    }
}