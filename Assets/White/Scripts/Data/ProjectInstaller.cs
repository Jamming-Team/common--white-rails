using Reflex.Core;
using XTools;

namespace White {
    public class ProjectInstaller : ProjectInstallerBase {
        public override void InstallBindings(ContainerBuilder builder) {
            base.InstallBindings(builder);

            builder.AddSingleton(new DataManager(), typeof(DataManagerBase), typeof(DataManager));
        }
    }
}