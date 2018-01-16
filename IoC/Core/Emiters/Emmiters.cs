namespace IoC.Core.Emiters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class Emmiters
    {
        [NotNull]
        public static Emmiter CreateEmitter([NotNull] this ILGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return new Emmiter(generator);
        }

        [NotNull]
        public static LocalBuilder DeclareLocal([NotNull] this Emmiter emitter, Type localType, bool pinned = false)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            return emitter.Generator.DeclareLocal(localType, pinned);
        }

        [NotNull]
        public static Emmiter Newobj([NotNull] this Emmiter emitter, [NotNull] ConstructorInfo constructorInfo)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (constructorInfo == null) throw new ArgumentNullException(nameof(constructorInfo));
            emitter.Generator.Emit(OpCodes.Newobj, constructorInfo);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldloc([NotNull] this Emmiter emitter, [NotNull] LocalBuilder local)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (local == null) throw new ArgumentNullException(nameof(local));
            emitter.Generator.Emit(OpCodes.Ldloc, local);
            return emitter;
        }

        [NotNull]
        public static Emmiter Stloc([NotNull] this Emmiter emitter, [NotNull] LocalBuilder local)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (local == null) throw new ArgumentNullException(nameof(local));
            emitter.Generator.Emit(OpCodes.Stloc, local);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldarg([NotNull] this Emmiter emitter, int index)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            switch (index)
            {
                case 0:
                    emitter.Generator.Emit(OpCodes.Ldarg_0);
                    break;

                case 1:
                    emitter.Generator.Emit(OpCodes.Ldarg_1);
                    break;

                case 2:
                    emitter.Generator.Emit(OpCodes.Ldarg_2);
                    break;

                case 3:
                    emitter.Generator.Emit(OpCodes.Ldarg_3);
                    break;

                default:
                    emitter.Generator.Emit(OpCodes.Ldarg, index);
                    break;
            }

            return emitter;
        }

        [NotNull]
        public static Emmiter Ret([NotNull] this Emmiter emitter)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ret);
            return emitter;
        }

        [NotNull]
        public static Emmiter Box([NotNull] this Emmiter emitter, [NotNull] Type type)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (type == null) throw new ArgumentNullException(nameof(type));
            emitter.Generator.Emit(OpCodes.Box, type);
            return emitter;
        }

        [NotNull]
        public static Emmiter Unbox_Any([NotNull] this Emmiter emitter, [NotNull] Type type)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (type == null) throw new ArgumentNullException(nameof(type));
            emitter.Generator.Emit(OpCodes.Unbox_Any, type);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldc([NotNull] this Emmiter emitter, int value)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ldc_I4, value);
            return emitter;
        }

        public static Emmiter Ldc([NotNull] this Emmiter emitter, long value)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ldc_I8, value);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldc([NotNull] this Emmiter emitter, float value)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ldc_R4, value);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldc([NotNull] this Emmiter emitter, double value)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ldc_R8, value);
            return emitter;
        }

        [NotNull]
        public static Emmiter Ldstr([NotNull] this Emmiter emitter, [CanBeNull] string value)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            if (value != null)
            {
                emitter.Generator.Emit(OpCodes.Ldstr, value);
            }
            else
            {
                emitter.Generator.Emit(OpCodes.Ldnull);
            }

            return emitter;
        }

        [NotNull]
        public static Emmiter Ldelem_Ref([NotNull] this Emmiter emitter)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Ldelem_Ref);
            return emitter;
        }

        public static Emmiter Ldobj<T>([NotNull] this Emmiter emitter, T value)
            where T : class
        {
            var gch = GCHandle.Alloc(value);
            var ptr = GCHandle.ToIntPtr(gch);

            if (IntPtr.Size == 4)
            {
                emitter.Generator.Emit(OpCodes.Ldc_I4, ptr.ToInt32());
            }
            else
            {
                emitter.Generator.Emit(OpCodes.Ldc_I8, ptr.ToInt64());
            }

            emitter.Generator.Emit(OpCodes.Ldobj, typeof(T));
            emitter.AddResource(Disposable.Create(() => { gch.Free(); }));
            return emitter;
        }

        [NotNull]
        public static Emmiter Call([NotNull] this Emmiter emitter, MethodInfo methodInfo)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.EmitCall(methodInfo.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, methodInfo, null);
            return emitter;
        }

        [NotNull]
        public static Emmiter Pop([NotNull] this Emmiter emitter)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            emitter.Generator.Emit(OpCodes.Pop);
            return emitter;
        }

        internal sealed class Emmiter : IDisposable
        {
            [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();

            public Emmiter([NotNull] ILGenerator generator)
            {
                Generator = generator ?? throw new ArgumentNullException(nameof(generator));
            }

            [NotNull] public readonly ILGenerator Generator;

            public void AddResource([NotNull] IDisposable resource)
            {
                if (resource == null) throw new ArgumentNullException(nameof(resource));
                _resources.Add(resource);
            }

            public void Dispose()
            {
                Disposable.Create(_resources).Dispose();
            }
        }
    }
}
