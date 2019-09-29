using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class NoTargetNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.Target != null ? 1 : 0;
        }
        
    }
}