namespace RenderCore
{
    public class EntityContainer : TickableContainer<IEntity>
    {
        protected override void Dispose(bool _disposing)
        {
            foreach (IEntity entity in this)
            {
                entity.Dispose();
            }

            base.Dispose(_disposing);
        }
    }
}