namespace Atividade02.Worker.Domain.Common;

public abstract class ValueObjects
{
    public virtual bool IsValid() => throw new NotImplementedException();
}