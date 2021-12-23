//using Framework.Utility.Extensions;
//using Framework.Utility.Reflection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Framework.Utility.Mapping
//{
//    public class AutoInjectFactory
//    {
//        public List<(Type, Type)> ConvertList { get; } = new List<(Type, Type)>();

  
//        public void AddAssemblys(params Assembly[] assemblys)
//        {
//            foreach (var assembly in assemblys)
//            {
//                var atributes = assembly.GetTypes()
//                    .Where(_type => _type.GetCustomAttribute<MapperInitAttribute>() != null)
//                    .Select(_type => _type.GetCustomAttribute<MapperInitAttribute>());

//                foreach (var atribute in atributes)
//                {
//                    ConvertList.Add((atribute.SourceType, atribute.TargetType));
//                }
//            }
//        }

//        /// <summary>
//        /// 执行对象映射构造
//        /// </summary>
//        [Obsolete]
//        public void AddAssemblys_bk(params Assembly[] assemblys)
//        {
//            //List<(Type Source, Type Target)> tuples = new List<(Type Source, Type Target)>();

//            Type[] types = AssemblyManager.FindTypesByAttribute<MapFromAttribute>();
//            foreach (Type targetType in types)
//            {
//                MapFromAttribute attribute = targetType.GetAttribute<MapFromAttribute>(true);
//                foreach (Type sourceType in attribute.SourceTypes)
//                {
//                    var tuple = ValueTuple.Create(sourceType, targetType);
//                    ConvertList.AddIfNotExist(tuple);
//                }
//            }

//            types = AssemblyManager.FindTypesByAttribute<MapToAttribute>();
//            foreach (Type sourceType in types)
//            {
//                MapToAttribute attribute = sourceType.GetAttribute<MapToAttribute>(true);
//                foreach (Type targetType in attribute.TargetTypes)
//                {
//                    var tuple = ValueTuple.Create(sourceType, targetType);
//                    ConvertList.AddIfNotExist(tuple);
//                }
//            }
             
//        }

//    }
//}
