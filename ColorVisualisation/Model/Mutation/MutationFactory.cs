using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Mutation
{
    class MutationFactory
    {
        public static BaseMutation Create(string mutationType)
        {
            if (mutationType == Resources.BitMutation)
                return new BitMutation();
            if (mutationType == Resources.ValueMutation)
                return new ValueMutation();
            return null;
        }
    }
}
