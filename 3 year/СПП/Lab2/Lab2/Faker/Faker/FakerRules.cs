using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Generator;
using Generator.Default;

namespace Faker.Rules
{
    public sealed class FakerRules
    {
        private Dictionary<Type, ValueGenerator> _typeRules;
        private Dictionary<string, ValueGenerator> _propRules;

        public FakerRules()
        {
            this._typeRules = new Dictionary<Type, ValueGenerator>();
            this._propRules = new Dictionary<string, ValueGenerator>();
            this.AddTypeRule<sbyte, Int8Generator>();
            this.AddTypeRule<short, Int16Generator>();
            this.AddTypeRule<int, Int32Generator>();
            this.AddTypeRule<long, Int64Generator>();
            this.AddTypeRule<byte, UInt8Generator>();
            this.AddTypeRule<ushort, UInt16Generator>();
            this.AddTypeRule<uint, UInt32Generator>();
            this.AddTypeRule<ulong, UInt64Generator>();
            this.AddTypeRule<float, FloatGenerator>();
            this.AddTypeRule<double, DoubleGenerator>();
            this.AddTypeRule<char, CharGenerator>();
            this.AddTypeRule<bool, BoolGenerator>();
            this.AddTypeRule<string, StringGenerator>();
        }

        public void AddTypeRule<TType, TGenerator>()
        {
            var ctor = typeof(TGenerator).GetConstructor(new Type[] { });
            if (ctor == null)
                throw new ArgumentNullException("Cannot instanciate generator!");
            var generator = ctor.Invoke(new object[] { }) as ValueGenerator;
            if (generator == null)
                throw new ArgumentNullException("Cannot instanciate generator!");
            this._typeRules[typeof(TType)] = generator;
        }

        public void AddPropRule<TType, TPropType, TGenerator>(
            Expression<Func<TType, TPropType>> prop)
        {
            var ctor = typeof(TGenerator).GetConstructor(new Type[] { });
            if (ctor == null)
                throw new ArgumentNullException("Cannot instanciate generator!");
            var generator = ctor.Invoke(new object[] { }) as ValueGenerator;
            if (generator == null)
                throw new ArgumentNullException("Cannot instanciate generator!");
            if (prop.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentNullException("Cannot parse expression if it is not MemberAccess!");
            this._propRules[(prop.Body as MemberExpression).Member.Name] = generator;
        }

        public ValueGenerator GetGenerator(Type forType)
        {
            if (!this._typeRules.ContainsKey(forType))
                return null;
            return this._typeRules[forType];
        }

        public ValueGenerator GetGenerator(string forMember)
        {
            if (!this._propRules.ContainsKey(forMember))
                return null;
            return this._propRules[forMember];
        }
    }
}
