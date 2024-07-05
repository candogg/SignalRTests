namespace Project.Shared.Services.Base
{
    public class ServiceSingularBase<T> where T : class, new()
    {
        private static T? derivedObject;

        public static T DerivedObject
        {
            get
            {
                derivedObject ??= Activator.CreateInstance<T>();

                return derivedObject;
            }
        }
    }
}
