using Leopotam.Ecs;

namespace UsefulScripts.Other
{
    public static class EcsS
    {
        public static void TryDel<T>(this EcsEntity ecsEntity) where T : struct
        {
            if (ecsEntity.Has<T>())
                ecsEntity.Del<T>();
        }

        public static bool TryGet<T>(this EcsEntity ecsEntity, out T component) where T : struct
        {
            if (ecsEntity.Has<T>())
            {
                component = ecsEntity.Get<T>();
                return true;
            }

            component = default;
            return false;
        }

        public static void Del1<T1, T2>(this EcsFilter<T1, T2> ecsFilter, int i) where T1 : struct where T2 : struct
        {
            ecsFilter.GetEntity(i).Del<T1>();
        }

        public static void Del2<T1, T2>(this EcsFilter<T1, T2> ecsFilter, int i) where T1 : struct where T2 : struct
        {
            ecsFilter.GetEntity(i).Del<T2>();
        }

        public static T GetSystem<T>(this EcsSystems ecsSystems) where T : IEcsSystem
        {
            var systems = ecsSystems.GetAllSystems();
            var list = systems.Items;
            var i = ecsSystems.GetNamedRunSystem(typeof(T).Name);
            return (T) list[i];
        }

        public static void SetSystem<T>(out T ecsSystem, EcsSystems ecsSystems) where T : IEcsSystem
        {
            ecsSystem = ecsSystems.GetSystem<T>();
        }
    }
}