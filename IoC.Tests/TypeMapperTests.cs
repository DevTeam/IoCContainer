#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Shouldly;
    using Xunit;

    public class TypeMapperTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldMapType(Type type, Type targetType, IDictionary<Type, Type> expectedMap)
        {
            // Given
            var mapper = TypeMapper.Shared;
            IDictionary<Type, Type> actualMap = new Dictionary<Type, Type>();

            // When
            mapper.Map(type, targetType, actualMap);

            // Then
            actualMap.OrderBy(i => i.Key.FullName).ToArray().ShouldBe(expectedMap.OrderBy(i => i.Key.FullName).ToArray());
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] { typeof(TT), typeof(string), new Dictionary<Type, Type> { { typeof(TT), typeof(string) } }},
            new object[] { typeof(string), typeof(string), new Dictionary<Type, Type>()},
            new object[] { typeof(IList<TT>), typeof(IList<string>), new Dictionary<Type, Type> { { typeof(TT), typeof(string) }, { typeof(IList<TT>), typeof(IList<string>) } }},
            new object[] { typeof(IList<TT>), typeof(List<string>), new Dictionary<Type, Type> { { typeof(TT), typeof(string)}, { typeof(IList<TT>), typeof(IList<string>) }, { typeof(ICollection<TT>), typeof(ICollection<string>) }, { typeof(IEnumerable<TT>), typeof(IEnumerable<string>) } }},
            new object[] { typeof(IList<>), typeof(IList<>), new Dictionary<Type, Type>() },
            new object[] { typeof(IList<TT>), typeof(IList<>), new Dictionary<Type, Type>( ) },
            new object[] { typeof(IList<>), typeof(IList<string>), new Dictionary<Type, Type>{ { typeof(IList<>), typeof(IList<string>)} } },
            new object[] { typeof(IList<string>), typeof(IList<string>), new Dictionary<Type, Type>( ) },
            new object[] { typeof(IList<ISet<TT>>), typeof(List<ISet<string>>), new Dictionary<Type, Type> { { typeof(TT), typeof(string) }, { typeof(ISet<TT>), typeof(ISet<string>) }, { typeof(IList<ISet<TT>>), typeof(IList<ISet<string>>) }, { typeof(IEnumerable<ISet<TT>>), typeof(IEnumerable<ISet<string>>) }, { typeof(ICollection<ISet<TT>>), typeof(ICollection<ISet<string>>) } }},
            new object[] { typeof(IList<IDictionary<TT1, TT2>>), typeof(List<IDictionary<string, int>>), new Dictionary<Type, Type> { { typeof(TT1), typeof(string) }, { typeof(TT2), typeof(int) }, { typeof(IDictionary<TT1, TT2>), typeof(IDictionary<string, int>) }, { typeof(IList<IDictionary<TT1, TT2>>), typeof(IList<IDictionary<string, int>>) }, { typeof(IEnumerable<IDictionary<TT1, TT2>>), typeof(IEnumerable<IDictionary<string, int>>) }, { typeof(ICollection<IDictionary<TT1, TT2>>), typeof(ICollection<IDictionary<string, int>>) } }},
            new object[] { typeof(TT[]), typeof(string[]), new Dictionary<Type, Type> { { typeof(TT), typeof(string) }, { typeof(TT[]), typeof(string[]) } }},
            new object[] { typeof(IList<IDictionary<TT1, TT2[]>>), typeof(List<IDictionary<string, int[]>>), new Dictionary<Type, Type> { { typeof(TT1), typeof(string) }, { typeof(TT2), typeof(int) }, { typeof(TT2[]), typeof(int[]) }, { typeof(IDictionary<TT1, TT2[]>), typeof(IDictionary<string, int[]>) }, { typeof(IList<IDictionary<TT1, TT2[]>>), typeof(IList<IDictionary<string, int[]>>) }, { typeof(IEnumerable<IDictionary<TT1, TT2[]>>), typeof(IEnumerable<IDictionary<string, int[]>>) }, { typeof(ICollection<IDictionary<TT1, TT2[]>>), typeof(ICollection<IDictionary<string, int[]>>) } }},
            new object[] { typeof(List<TT>), typeof(IList<string>), new Dictionary<Type, Type> { { typeof(TT), typeof(string) }, { typeof(IList<TT>), typeof(IList<string>) }, { typeof(ICollection<TT>), typeof(ICollection<string>) }, { typeof(IEnumerable<TT>), typeof(IEnumerable<string>) } }},
        };
    }
}
#endif