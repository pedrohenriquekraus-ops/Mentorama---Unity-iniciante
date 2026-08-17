using System.Collections.Generic;
using UnityEngine;



public class Dot
{

    public Vector3 CurrentPosition { get; set; }
    public Vector3 LastPosition { get; set; }
    public bool IsLocked { get; set; }

    public List<Connection> Connections { get; } = new List<Connection>();



    public Dot(Vector3 currentPosition, bool isLocked)
    {
        CurrentPosition = currentPosition;
        LastPosition = currentPosition;
        IsLocked = isLocked;

    }


    public static Connection connect(Dot dota, Dot dotb, float length = -1f)
    {

        Connection connection;

        connection = length < 0f ? new Connection(dota, dotb) : new Connection(dota, dotb, length);

        dota.Connections.Add(connection);
        dotb.Connections.Add(connection);

        return connection;

    }


    public static void Disconnect(Connection connection)
    {
        List<Connection> DotaConnections = connection.Dota.Connections;
        List<Connection> DotbConnections = connection.DotB.Connections;



        if (DotaConnections.Contains(connection)) connection.Dota.Connections.Remove(connection);
        if (DotbConnections.Contains(connection)) connection.DotB.Connections.Remove(connection);
    }
}



