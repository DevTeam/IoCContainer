namespace WcfServiceLibrary
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceModel.Configuration;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class IocExtensionElement: BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(IocAttribute);

        protected override object CreateBehavior() => new IocAttribute();
    }
}