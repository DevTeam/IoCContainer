// ReSharper disable UnusedMethodReturnValue.Global
namespace IoC.Benchmark
{
    using System;
    using Castle.Windsor;
    using global::LightInject;
    using global::Ninject;
    using global::Unity;
    using Model;

    public static class Configs
    {
        public static readonly object [] EmptyArgs = Array.Empty<object>();
        public static readonly Type RootType = TestTypeBuilder.Default.RootType;

        public static readonly Container IoCContainerTransient = IoCContainer.Transient();
        public static readonly Container IoCContainerSingleton = IoCContainer.Singleton();
        public static readonly Container IoCContainerComplex = IoCContainer.Complex();
        public static readonly Resolver<IService1> IoCContainerTransientResolve = IoCContainerTransient.GetResolver<IService1>();
        public static readonly Resolver<IService1> IoCContainerSingletonResolve = IoCContainerSingleton.GetResolver<IService1>();
        public static readonly Resolver<object> IoCContainerComplexResolve = IoCContainerComplex.GetResolver<object>(TestTypeBuilder.Default.RootType);

        public static readonly global::Autofac.IContainer AutofacTransient = Autofac.Transient();
        public static readonly global::Autofac.IContainer AutofacSingleton = Autofac.Singleton();
        public static readonly global::Autofac.IContainer AutofacComplex = Autofac.Complex();

        public static readonly WindsorContainer CastleWindsorTransient = CastleWindsor.Transient();
        public static readonly WindsorContainer CastleWindsorSingleton = CastleWindsor.Singleton();
        public static readonly WindsorContainer CastleWindsorComplex = CastleWindsor.Complex();

        public static readonly global::DryIoc.Container DryIocTransient = DryIoc.Transient();
        public static readonly global::DryIoc.Container DryIocSingleton = DryIoc.Singleton();
        public static readonly global::DryIoc.Container DryIocComplex = DryIoc.Complex();

        public static readonly ServiceContainer LightInjectTransient = LightInject.Transient();
        public static readonly ServiceContainer LightInjectSingleton = LightInject.Singleton();
        public static readonly ServiceContainer LightInjectComplex = LightInject.Complex();

        public static readonly StandardKernel NinjectTransient = Ninject.Transient();
        public static readonly StandardKernel NinjectSingleton = Ninject.Singleton();
        public static readonly StandardKernel NinjectComplex = Ninject.Complex();

        public static readonly UnityContainer UnityTransient = Unity.Transient();
        public static readonly UnityContainer UnitySingleton = Unity.Singleton();
        public static readonly UnityContainer UnityComplex = Unity.Complex();

        public const int Series = 10000;
        public const int ComplexSeries = 10000;
    }
}