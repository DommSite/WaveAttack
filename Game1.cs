﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<BaseClass> entities = new List<BaseClass>();
    private BulletSystem bulletSystem;
    Texture2D bullet;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        GameManager.Instance.Initialize(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        GameManager.Instance.LoadContent(Content);


        /*Texture2D pixel;
        Texture2D bullet;
        Texture2D player;

        pixel = new Texture2D(GraphicsDevice,1,1);
        pixel.SetData(new Color[]{Color.White});

        bullet = Content.Load<Texture2D>("bullet");
        BulletSystem.CreateInstance(bullet);

        player = Content.Load<Texture2D>("Red_Arrow_Left");

        entities.Add(new Player(player));
        entities.Add(new Enemy(pixel, new Vector2(400,380), entities));*/
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        GameManager.Instance.Update(gameTime);
        /*
        foreach(var entity in entities){
            entity.Update();
        }
        BulletSystem.Instance.Update();*/

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        GameManager.Instance.Draw(_spriteBatch);
        /*
        foreach(var entity in entities){
            entity.Draw(_spriteBatch);
        }
        BulletSystem.Instance.Draw(_spriteBatch);*/

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
