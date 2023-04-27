//Simpel Grafik
//Movement
//Collision
//Gravitation
//BOSS
//Speciallised movement
//Sprites



using Raylib_cs;
using System.Numerics;

//VSYNC FIXAR SCREEN TEAR
Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);

//Windows Size
Raylib.InitWindow(1920, 1080, "Cataclysm");
Raylib.SetTargetFPS(60);
Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);

//Currentscene
string currentScene = "place";

//Speed
float playerspeed = 7f;

float playerfallspeed = 0;
float gravity = 2;

float playerJumpForce = 20;

//Camera
Camera2D camera = new Camera2D();

//List för alla väggar 
List<Rectangle> walls = new List<Rectangle>();

walls.Add(new Rectangle(0, 0, Raylib.GetScreenWidth(), 20));
walls.Add(new Rectangle(0, 1060, Raylib.GetScreenWidth(), 20));
walls.Add(new Rectangle(1900, 10, 20, 1070));
walls.Add(new Rectangle(0, 10, 20, 1070));


Rectangle player = new Rectangle(900, 500, 50, 50);

Vector2 playerlatePos = new Vector2(player.x, player.y);

while (!Raylib.WindowShouldClose())
{

    Logic();

    Collision();

    camera.target = playerlatePos;

    Graphics();
}

void Logic()
{
    if (currentScene == "place")
    {
        playerlatePos = new Vector2(player.x, player.y);

        playerfallspeed += gravity;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            playerfallspeed = -playerJumpForce;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            player.x += playerspeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            player.x -= playerspeed;
        }

        player.y += playerfallspeed;


        camera.target = playerlatePos;
        camera.offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        camera.rotation = 0;
        camera.zoom = 1;



    }
}
void Graphics()
{
    //GRAFIK
    Raylib.BeginDrawing();
    //BEGIN CAMERA
    Raylib.BeginMode2D(camera);

    Raylib.ClearBackground(Color.LIGHTGRAY);

    Raylib.DrawRectangleRec(player, Color.BLACK);


    foreach (Rectangle wall in walls)
    {
        Raylib.DrawRectangleRec(wall, Color.GRAY);
    }


    //END CAMERA
    Raylib.EndMode2D();

    Raylib.EndDrawing();
}
//Collision för väggarna runt arenan.
//Collision for loop
void Collision()
{

    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionRecs(player, wall))
        {
            player.x = playerlatePos.X;
            player.y = playerlatePos.Y;

            playerfallspeed = 0;
        }
    }

}
