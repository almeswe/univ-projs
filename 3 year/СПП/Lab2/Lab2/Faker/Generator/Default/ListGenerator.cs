using System;
using System.Collections.Generic;

namespace Generator.Default
{
    public sealed class ListGenerator : ValueGenerator
    {
        public ListGenerator() : base(typeof(IList<object>)) { }

        public override object Generate(Type type, GeneratorContext context)
        {
            var ctor = type.GetConstructor(new Type[] { });
            var instance = ctor.Invoke(new object[] { });
            var instanceSize = (int)context.Faker.Create<byte>(context.Rules);
            var addMethod = type.GetMethod("Add");
            var addMethodParam = addMethod.GetParameters()[0];
            for (int i = 0; i < instanceSize; i++)
                addMethod.Invoke(instance, new object[1] {
                    context.Faker.Create(addMethodParam.ParameterType, context.Rules) });
            return instance;
        }
    }
}