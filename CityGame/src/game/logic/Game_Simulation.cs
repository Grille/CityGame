namespace CityGame
{
    public partial class Game
    {
        void simulate()
        {
            int loops = (World.Width + World.Height) / 64;
        }
        void updateSimulation()
        {

        }

        public void UpdateField(int pos)
        {
            if (World.ReferenceX[pos] != 0 || World.ReferenceY[pos] != 0) return;
            byte typ = World.Typ[pos];
            int result = TestAreaDependet(typ, pos);
            int result2 = TestResourcesDependet(typ);
            var obj = Objects[typ];
            if (obj.OnUpdate != null) obj.OnUpdate();
            if (result < result2) result = result2;
            if (result == 0) return;
            else if (result == 1) replaceTyp(typ, pos, obj.UpgradeTyp);
            else if (result == 2) replaceTyp(typ, pos, obj.DowngradeTyp);
            else if (result == 3) replaceTyp(typ, pos, obj.DemolitionTyp);
            else if (result == 4) replaceTyp(typ, pos, obj.DecayTyp);
            else if (result == 5) replaceTyp(typ, pos, obj.DestroyTyp);
            else if (result == 6) Clear(pos);
            for (int i = 0; obj.ResourcesEffect != null && i < obj.ResourcesEffect.Length / 3; i++)
            {
                if (obj.ResourcesEffect[i * 3 + 0] == result)
                {
                    Resources[obj.ResourcesEffect[i * 3 + 1]].Value += obj.ResourcesEffect[i * 3 + 2];
                }
            }
            return;
        }


    }
}