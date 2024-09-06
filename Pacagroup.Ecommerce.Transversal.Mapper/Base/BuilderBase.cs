namespace Pacagroup.Ecommerce.Transversal.Mapper.Base
{
    public abstract class BuilderBase<TEntity, TDTO>
    {
        public abstract TEntity Convert(TDTO param);
        public abstract TDTO Convert(TEntity param);

        public List<TDTO> Convert(List<TEntity> param)
        {
            List<TDTO> response = new List<TDTO>();
            response = param.Select(o => Convert(o)).ToList();

            return response;
        }

        public List<TEntity> Convert(List<TDTO> param)
        {
            List<TEntity> response = new List<TEntity>();
            response = param.Select(o => Convert(o)).ToList();

            return response;
        }

    }
}