using System;
namespace MusicStoreMobile.Core.ViewModelResults
{
    public class DestructionResult
    {
        public bool Destroyed { get; set; }
    }

    public class DestructionResult<TEntity> : DestructionResult
    {
        public TEntity Entity { get; set; }
    }
}