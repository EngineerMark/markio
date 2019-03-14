using System.Collections.Generic;

public class ActorManager : Manager
{
    private static Dictionary<int, Actor> actors;

    public ActorManager()
    {
        actors = new Dictionary<int, Actor>();
    }

    public static void RegisterActor(Actor actor)
    {
        actors.Add(actor.gameObject.GetInstanceID(), actor);
    }

    public static Actor GetActor(int ID)
    {
        if (actors.ContainsKey(ID))
            return actors[ID];
        return null;
    }

    public static void Reset()
    {
        foreach(KeyValuePair<int, Actor> actor in actors)
        {
            actor.Value.gameObject.SetActive(true);
            actor.Value.isDead = false;
            actor.Value.transform.position = actor.Value.spawnPoint;
            actor.Value.healthPoints = actor.Value.start_healthPoints;
            actor.Value.DPS = actor.Value.start_dps;
        }
    }
}
