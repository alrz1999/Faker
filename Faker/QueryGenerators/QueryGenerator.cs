namespace Faker.QueryGenerators
{
    public abstract class QueryGenerator
    {
        protected readonly string field;

        protected QueryGenerator(string field)
        {
            this.field = field.ToLower();
        }
    }
}
