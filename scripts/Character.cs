using Godot;
using System;

public partial class Character : CharacterBody2D
{
	private const float _speed = 100.0f;
	private const float _jumpSpeed = -400.0f;
	private const float RotationSpeed = 3f;
	private const float Deceleration = 0.5f;
	private const float PI = 3.1415926535897931f;
	

	// Get the gravity from the project settings so you can sync with rigid body nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		velocity.Y += Gravity * (float)delta;

		// Handle jump.
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
			velocity.Y = _jumpSpeed;

		// Get the input direction.
		float direction = Input.GetAxis("ui_left", "ui_right");
		
		velocity.X = direction * _speed;

		// Get the input direction for rotating
		
		bool rotateLeft = Input.IsActionPressed("ui_rotate_left");
		bool rotateRight = Input.IsActionPressed("ui_rotate_right");

		//Handling rotating to the left side
		if (rotateLeft && IsOnFloor() && !rotateRight)
		{
			Rotation -= RotationSpeed * (float)delta;
			
			if ( -PI/2 < Rotation && Rotation < 0 ){
			velocity += new Vector2(-1.1f, 0).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( -PI < Rotation && Rotation < -PI/2 ){
			velocity += new Vector2(0, -1.1f).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( PI/2 < Rotation && Rotation < PI ){
			velocity += new Vector2(1.1f, 0).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( 0 < Rotation && Rotation < PI/2 ){
			velocity += new Vector2(0, 1.1f).Rotated(Rotation) * 0.5f * _speed;
			}
		}
		//Handling rotating to the right side
		if (rotateRight && IsOnFloor() && !rotateLeft)
		{
			Rotation += RotationSpeed * (float)delta;
			
			if ( -PI/2 < Rotation && Rotation < 0 ){
			velocity += new Vector2(0, 1.1f).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( -PI < Rotation && Rotation < -PI/2 ){
			velocity += new Vector2(-1.1f, 0).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( PI/2 < Rotation && Rotation < PI ){
			velocity += new Vector2(0, -1.1f).Rotated(Rotation) * 0.5f * _speed;
			}
			else if ( 0 < Rotation && Rotation < PI/2 ){
			velocity += new Vector2(1.1f, 0).Rotated(Rotation) * 0.5f * _speed;
			}
		}
		
		
		//handling auto rotation when no rotate inpits are active
		if (!rotateRight && IsOnFloor() && !rotateLeft){
			
			if ( -PI/4 < Rotation & Rotation < 0 ){
			Rotation = Mathf.Lerp(Rotation, 0, RotationSpeed * (float)delta);
			
			}
			else if ( 0 < Rotation && Rotation < PI/4 ){
			Rotation = Mathf.Lerp(Rotation, 0, RotationSpeed * (float)delta);
			
			}
			else if ( PI/4 < Rotation && Rotation < PI/2 ){
			Rotation = Mathf.Lerp(Rotation, PI/2, RotationSpeed * (float)delta);
			
			}
			else if ( PI/2 < Rotation && Rotation < 3*PI/4 ){
			Rotation = Mathf.Lerp(Rotation, PI/2, RotationSpeed * (float)delta);
			
			}
			else if ( 3*PI/4 < Rotation && Rotation < PI ){
			Rotation = Mathf.Lerp(Rotation, PI, RotationSpeed * (float)delta);
			
			}
			else if ( -PI < Rotation && Rotation < -3*PI/4 ){
			Rotation = Mathf.Lerp(Rotation, -PI, RotationSpeed * (float)delta);
			
			}
			else if ( -3*PI/4 < Rotation && Rotation < -PI/2 ){
			Rotation = Mathf.Lerp(Rotation, -PI/2, RotationSpeed * (float)delta);
			
			}
			else if ( -PI/2 < Rotation && Rotation < -PI/4 ){
			Rotation = Mathf.Lerp(Rotation, -PI/2, RotationSpeed * (float)delta);
			
			}
		}
		
		
			
			
			
			
		
		Velocity = velocity;
		MoveAndSlide();
		
	}

	
}
