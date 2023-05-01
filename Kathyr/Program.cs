//Simpel Grafik
//Movement
//Collision
//Gravitation
//BOSS
//Speciallised movement
//Sprites


//RAYLIB
using Raylib_cs;
using System.Numerics;

//VSYNC FIXAR SCREEN TEAR
Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);

//Windows Size
Raylib.InitWindow(1920, 1080, "Cataclysm");
//FRAMERATE
Raylib.SetTargetFPS(60);
//FULLSCREEN
Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);

//Currentscene
string currentScene = "menu";

//Speed
float playerspeed = 7f;

//lavaspeed
float lavaspeed = 6f;

//CUTSCENE LAVA SPEED
float cutlavaspeed = 2f;

//PLAYER FALL SPEED
float playerfallspeed = 0;

//GRAVITY
float gravity = 2;

//JUMPFORCE
float playerJumpForce = 20;


//NY 2D CAMERA
Camera2D camera = new Camera2D();

//LIST FÖR ALLA VÄGGAR
List<Rectangle> walls = new List<Rectangle>();

//ALLA VÄGGAR SOM GÖR BANAN
walls.Add(new Rectangle(0, 0, 820, 20));
walls.Add(new Rectangle(1250, 0, 670, 20));
walls.Add(new Rectangle(0, 1060, Raylib.GetScreenWidth(), 20));
walls.Add(new Rectangle(1900, 10, 20, 1070));
walls.Add(new Rectangle(0, 10, 20, 1070));
walls.Add(new Rectangle(1250, -1000, 20, 1000));
walls.Add(new Rectangle(800, -1000, 20, 1000));
walls.Add(new Rectangle(1250, -1500, 20, 500));
walls.Add(new Rectangle(-750, -1500, 2000, 20));
walls.Add(new Rectangle(-1300, -1000, 2100, 20));
walls.Add(new Rectangle(-1300, -2000, 20, 1000));
walls.Add(new Rectangle(-1300, -2000, 1200, 20));
walls.Add(new Rectangle(400, -2500, 20, 1000));
walls.Add(new Rectangle(-120, -3000, 20, 1000));
walls.Add(new Rectangle(-120, -3000, 1500, 20));
walls.Add(new Rectangle(420, -2500, 1500, 20));
walls.Add(new Rectangle(1900, -4500, 20, 2000));
walls.Add(new Rectangle(1360, -4500, 20, 1500));
walls.Add(new Rectangle(1360, -4500, 200, 20));
walls.Add(new Rectangle(1710, -4500, 190, 20));
walls.Add(new Rectangle(1710, -5500, 20, 1000));
walls.Add(new Rectangle(1540, -5500, 20, 1000));
walls.Add(new Rectangle(1540, -5500, 190, 20));

//PLAYER 
Rectangle player = new Rectangle(1000, 1000, 50, 50);

//PLAYER X OCH Y
Vector2 playerlatePos = new Vector2(player.x, player.y);

// LAVA
Rectangle lava = new Rectangle(-10000, 1400, 10000000, 1000);

//LAVA X OCH Y
Vector2 lavapos = new Vector2(lava.x, lava.y);

//DIAMOND
Rectangle diamond = new Rectangle(1625, -4300, 25, 25);

//VÄGGEN I LORE
Rectangle lorewall = new Rectangle(0, 0, 450, 1500);

// VÄGG NR 2 I LORE
Rectangle lorewall2 = new Rectangle(1530, 0, 400, 1500);

//CUTESCENE VÄGGAR
Rectangle cutwall = new Rectangle(0, 600, 400, 20);

//PLAYER I CUTSCENES
Rectangle cutplayer = new Rectangle(0, 950, 50, 50);

//CUTSCENE PLAYER X OCH Y
Vector2 cutplayerpos = new Vector2(cutplayer.x, cutplayer.y);

//CUTSCENE DIAMOND
Rectangle cutdiamond = new Rectangle(10, 960, 25, 25);

//CUTSCENE DIAMOND X OCH Y
Vector2 cutdiamondpos = new Vector2(cutdiamond.x, cutdiamond.y);

//CUTSCENE MARK/GRÄS
Rectangle cutgrass = new Rectangle(0, 1050, 2000, 50);

//HIMLEN I CUTSCENES
Rectangle cutsky = new Rectangle(0, 0, 10000, 10000);

//GRÅ VÄGG I WIN CUTSCENE
Rectangle cutgraywall = new Rectangle(0, 600, 390, 500);

//PLAYER SOM DÖR I CUTSCENE
Rectangle cutplayerdie = new Rectangle(800, -10, 200, 200);

//PLAYER SOM DÖR I CUTSCENE X OCH Y
Vector2 cutplayerdiepos = new Vector2(cutplayerdie.x, cutplayerdie.y);

//LAVA I CUTSCENE
Rectangle cutlava = new Rectangle(0, 900, 2000, 10000000);

//CUTSCENE LAVA X OCH Y
Vector2 cutlavapos = new Vector2(cutlava.x, cutlava.y);

//SJÄLVA SPELET
while (!Raylib.WindowShouldClose())
{

    //CHECKAR COLLISIONEN MELLAN PLAYER OCH LAVA
    bool are0verlapping = Raylib.CheckCollisionRecs(player, lava);

    //CHECKAR COLLISIONEN MELLAN PLAYER OCH DIAMOND
    bool are0verlappingdiamond = Raylib.CheckCollisionRecs(player, diamond);

    //MENU SCENEN
    if (currentScene == "menu")
    {
        //OM MAN KLICKAR PÅ ENTER SÅ BYTER MAN TILL "INFO" SCENEN
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "info";
        }
    }
    // OM MAN KLICKAR PÅ ENTER I "INFO SCENEN SÅ BYTER DEN TILL GAMEPLAY SECTIONEN
    else if (currentScene == "info")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }
    //SJÄLVA GAMEPLAY KÅDEN
    else if (currentScene == "game")
    {
        //GRAVITATIONEN
        playerlatePos = new Vector2(player.x, player.y);

        //PLAYERS FALL SPEED = 0 + = 2 VILKET ÄR GRAVITY
        playerfallspeed += gravity;

        //DET HÄR GÖR SÅ ATT KARAKTÄREN KAN FLYGA
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            playerfallspeed = -playerJumpForce;
        }
        //BASIC PLAYER MOVEMENT
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            player.x += playerspeed;
        }
        //BASIC PLAYER MOVEMENT
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            player.x -= playerspeed;
        }

        //DEN HÄR KODEN GÖR SÅ ATT PLAYERN FALLER 
        player.y += playerfallspeed;

        //LAVA RÖRELSE
        lavapos = new Vector2(lava.x, lava.y);
        if (currentScene == "game")
            lava.y -= lavaspeed;


        //KODEN SÅ ATT KAMERAN FÖLJER EFTER PLAYERN
        camera.target = playerlatePos;
        //VAD KAMERAN VISAR
        camera.offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        //HUR MYCKET KAMERAN ÄR ROTERAD AKA 0
        camera.rotation = 0;
        //HUR ZOOMAD KAMERAN ÄR
        camera.zoom = 1;


    }




    //COLLISION FOR LOOP
    foreach (Rectangle wall in walls)
    {
        //COLLISIONEN CHECKAR OM PLAYERN RÖR VÄGGEN OCH OM DET ÄR TRUE SÄTTER PLAYERN PÅ FÖRRA FRAMEN NÄR MAN INTE NUDDADE VÄGGEN
        if (Raylib.CheckCollisionRecs(player, wall))
        {
            //COLLISION X OCH Y
            player.x = playerlatePos.X;
            player.y = playerlatePos.Y;

            //SÅ ATT ENS FALLSPED STANNAR NÄR MAN NUDDAR VÄGGAR
            playerfallspeed = 0;
        }
    }
    
    //OM MAN RÖR PÅ DIAMOND SÅ BYTER SCENEN TILL WIN SCENEN
    if (currentScene == "game")
    {
        if (are0verlappingdiamond == true)
        {
            currentScene = "win";
        }

    }

    //BASICALLY CUTSCENE FÖR WIN SCENEN
    if (currentScene == "win")
    {
        //INGET HÄR KAN KONTROLERAS AV SPELAREN UTAN ÄR BARA MOVEMENT FÖR CUTSCENEPLAYER OCH CUTSCENEDIAMOND
        cutplayerpos = new Vector2(cutplayer.x, cutplayer.y);
        if (currentScene == "win")
        cutplayer.x += playerspeed;
        cutplayer.y -= playerspeed;

        cutdiamondpos = new Vector2(cutdiamond.x, cutdiamond.y);
        if (currentScene == "win")
            cutdiamond.x += playerspeed;
        cutdiamond.y -= playerspeed;

        //OM DU KLICKAR PÅENTER SÅ KOMMER DU TILLBAKS TILL MENYN
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "menu";
        }
    }

    //OM DU RÖR PÅ LAVAN SÅ BYTER SCENEN TILL DEATH SCENEN
    if (currentScene == "game")
    {
        if (are0verlapping == true)
        {
            currentScene = "die";
        }
    }

    //CUTSCENE FÖR DEATH SCENEN DÄR LAVAN ÅKER UPPÅT OCH PLAYERN ÅKER NER
    if (currentScene == "die")
    {
        cutplayerdiepos = new Vector2(cutplayerdie.x, cutplayerdie.y);
        if (currentScene == "die")
            cutplayerdie.y += playerspeed;

        cutlavapos = new Vector2(cutlava.x, cutlava.y);
        if (currentScene == "die")
            cutlava.y -= cutlavaspeed;


    }

    //GRAFIK
    Raylib.BeginDrawing();

    //BAKRUNDEN FÖR VARJE SCEN ÄR LIGHTGRAY
    Raylib.ClearBackground(Color.LIGHTGRAY);

    //ALL TEXT I MENYN
    if (currentScene == "menu")
    {
        Raylib.DrawText("HAPPY WHEELS", 570, 300, 64, Color.BLACK);
        Raylib.DrawText("MATRIX", 1100, 300, 64, Color.DARKGREEN);
        Raylib.DrawText("RESSURECTED", 610, 350, 100, Color.RED);
        Raylib.DrawText("PRESS ENTER TO START", 570, 500, 64, Color.GRAY);
        Raylib.DrawText("PRESS ESC TO QUIT", 570, 600, 64, Color.GRAY);
        Raylib.DrawText("W/UP", 20, 900, 64, Color.GRAY);
        Raylib.DrawText("A/LEFT", 20, 950, 64, Color.GRAY);
        Raylib.DrawText("D/RIGHT", 20, 1000, 64, Color.GRAY);
    }
    // ALL TEXT FÖR "INFO" SCENEN
    if (currentScene == "info")
    {
        Raylib.DrawRectangleRec(lorewall, Color.BLACK);
        Raylib.DrawRectangleRec(lorewall2, Color.BLACK);
        Raylib.DrawText("LORE", 870, 50, 100, Color.BLACK);
        Raylib.DrawText("THE DIAMOND IS STOLEN AND", 490, 150, 64, Color.BLACK);
        Raylib.DrawText("YOU'RE LEGS ARE BROKEN", 545, 220, 64, Color.BLACK);
        Raylib.DrawText("BUT LUCKILY YOU HAVE THE", 490, 290, 64, Color.BLACK);
        Raylib.DrawText("ULTIMATE JETPACK THAT", 545, 360, 64, Color.BLACK);
        Raylib.DrawText("WILL SAVE YOU FROM THE", 520, 430, 64, Color.BLACK);
        Raylib.DrawText("INCOMING LAVA THAT WILL", 520, 500, 64, Color.BLACK);
        Raylib.DrawText("BURN YOU ALIVE, TAKE BACK", 500, 570, 64, Color.BLACK);
        Raylib.DrawText("THE DIAMOND AT ANY COST", 520, 640, 64, Color.BLACK);
        Raylib.DrawText("PRESS ENTER TO START", 480, 850, 77, Color.RED);
    }
    //ALL GRAFIK OCH TEXT FÖR WIN SCENEN
    if (currentScene == "win")
    {
        Raylib.DrawRectangleRec(cutsky, Color.SKYBLUE);
        Raylib.DrawRectangleRec(cutgraywall, Color.LIGHTGRAY);
        Raylib.DrawRectangleRec(cutwall, Color.GRAY);
        Raylib.DrawRectangleRec(cutplayer, Color.BLACK);
        Raylib.DrawRectangleRec(cutdiamond, Color.BLUE);
        Raylib.DrawRectangleRec(cutgrass, Color.DARKGREEN);
        Raylib.DrawText("YOU WIN", 10, 20, 100, Color.DARKBLUE);
        Raylib.DrawText("PRESS ENTER FOR MENU", 10, 120, 100, Color.DARKBLUE);
    }
    //ALL TEXT OCH GRAFIK FÖR DEATH SCENEN
    if (currentScene == "die")
    {
        Raylib.DrawRectangleRec(cutplayerdie, Color.BLACK);
        Raylib.DrawRectangleRec(cutlava, Color.ORANGE);
        Raylib.DrawText("YOU DIED", 725, 850, 100, Color.RED);
        Raylib.DrawText("PRESS ESC TO QUIT", 450, 950, 100, Color.RED);
    }
    //ALL GRAFIK FÖR GAMEPLAYEN
    if (currentScene == "game")
    {
        //STARTAR SÅ ATT KAMERAN FÖLJER EFTER SPELAREN I "GAME" SCENEN
        Raylib.BeginMode2D(camera);

        Raylib.DrawRectangleRec(player, Color.BLACK);

        Raylib.DrawRectangleRec(lava, Color.ORANGE);

        Raylib.DrawRectangleRec(diamond, Color.BLUE);

        //LÄGGER IN ALLA VÄGGAR FRÅN LISTAN
        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.GRAY);
        }

    }

    //END CAMERA 
    Raylib.EndMode2D();

    //END DRAWING
    Raylib.EndDrawing();
};