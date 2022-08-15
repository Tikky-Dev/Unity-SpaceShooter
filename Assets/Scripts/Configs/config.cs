using UnityEngine;

public static class config{
    // Playground limits:
    public static float upperLimit = 7f;
    public static float lowerLimit = -5.0f;
    public static float rightlimit = 10.0f;
    public static float leftlimit = -10.0f;
}

public static class playerConfig{
    // Starting params:
        // Position:
    public static float startXPosition = 0f;
    public static float startYPosition = 0f;
    public static float startZPosition = 0f;
    
        // Variables:
    public static float speed = 5.0f;
    public static int lives = 3;

    // Playground limits:
    public static float upperLimit = 4f;
    public static float lowerLimit = -4f;
    public static float rightlimit = 11.3f;
    public static float leftlimit = -11.3f;

}

public static class laseConfig{
    // Variables:
    public static float speed = 8.0f;
    public static float fireRate = 0.2f;
    public static Vector3 offsetSpawn = new Vector3(0f, 0.8f, 0f);
    public static float distanceLimit = 7f;
}

public static class enemyConfig{
    // Variables
    public static float speed = 4.0f;
    public static float spanRangeMin = 1.0f;
    public static float spanRangeMax = 2.0f;
}

public static class powerupConfig{
    public static float speed = 3.0f;
    public static float timeLimit = 5.0f;
    public static float spanRangeMin = 15.0f;
    public static float spanRangeMax = 25.0f;

}