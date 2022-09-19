using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Generator;

using Faker.Rules;
using Faker.Analyzer;
using Faker.Exceptions;
using Faker.Abstractions;
using Faker.Analyzer.Exceptions;

namespace Faker
{
    public sealed class Faker : IFaker
    {
        private FakerRules _rules;
        private FakerAnalyzer _analyzer;
        private GeneratorContext _context;

        public T Create<T>(FakerRules rules = null)
        {
            try
            {
                this._analyzer  = this._analyzer ?? new FakerAnalyzer();
                this._analyzer.Analyze<T>();
                return (T)this.Create(typeof(T), rules);
            }
            catch (GraphNetCircularDependencyException g)
            {
                throw new FakerCircularDependencyException(g);
            }
            catch (Exception e)
            {
                throw new FakerUnhandledException(e.Message);
            }
        }

        public object Create(Type type, FakerRules rules)
        {
            this._rules = rules ?? new FakerRules();
            this._context = new GeneratorContext(this, this._rules);
            var generator = rules.GetGenerator(type);
            if (generator != null)
                return generator.Generate(type, this._context);
            return this.CreateWithoutRule(type);
        }

        private object CreateWithoutRule(Type type)
        {
            var instanceContext = this.CreateInstance(type);
            this.CreateFields(type, instanceContext);
            this.CreateProperties(type, instanceContext);
            return instanceContext.Item1;
        }

        private void CreateFields(Type type, 
            Tuple<object, ParameterInfo[]> instanceContext)
        {
            var (instance, parameters) = instanceContext;
            foreach (var field in type.GetFields().Where(f => f.IsPublic))
            {
                if (this.IsCreatedInConstructor(field.Name, parameters))
                    continue;
                object fieldValue = this.CreateWithPropRule(
                    field.Name, field.FieldType);
                field.SetValue(instance, fieldValue);
            }
        }

        private void CreateProperties(Type type, 
            Tuple<object, ParameterInfo[]> instanceContext)
        {
            var (instance, parameters) = instanceContext;
            foreach (var property in type.GetProperties().Where(p => p.CanWrite))
            {
                if (this.IsCreatedInConstructor(property.Name, parameters))
                    continue;
                object propertyValue = this.CreateWithPropRule(
                    property.Name, property.PropertyType);
                property.SetValue(instance, propertyValue);
            }
        }

        private Tuple<object, ParameterInfo[]> CreateInstance(Type type)
        {
            var ctors = type.GetConstructors();
            var sortedCtors = ctors.OrderByDescending(
                c => c.GetParameters().Length);
            foreach (var ctor in sortedCtors)
            {
                var args = new List<object>();
                var parameters = ctor.GetParameters();
                try
                {
                    foreach (var parameter in parameters)
                        args.Add(this.CreateWithPropRule(parameter.Name, 
                            parameter.ParameterType));
                    var instance = ctor.Invoke(args.ToArray());
                    return Tuple.Create(instance, parameters);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            throw new FakerBadConstructorException(
                $"Cannot instanciate type {nameof(type)}");
        }

        private object CreateWithPropRule(string propName, Type propType)
        {
            propName = char.ToUpper(propName[0]) + propName.Substring(1);
            var generator = this._rules.GetGenerator(propName);
            if (generator == null)
                return this.Create(propType, this._rules);
            return generator.Generate(propType, this._context);
        }

        private bool IsCreatedInConstructor(string memberName, ParameterInfo[] parameters)
        {
            foreach (var parameter in parameters)
                if (parameter.Name.ToLower() == memberName.ToLower())
                    return true;
            return false;
        }
    }
}