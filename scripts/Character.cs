using Godot;
using System;

public partial class Character : CharacterBody2D
{
	private const float _speed = 100.0f;
	private const float _minJumpSpeed = 150.0f;
	private const float _maxJumpSpeed = 200.0f;
	private const float jumpTime = 0.32f;
	private const float RotationSpeed = 2f;
	private const float Deceleration = 0.5f;
	private const float PI = 3.1415926535897931f;
	

	// Get the gravity from the project settings so you can sync with rigid body nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private float _jumpTimer = 0;
	private bool _isJumping = false;
	
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		velocity.Y += Gravity * (float)delta;

		// Reset jump state when touching ground
		 if (IsOnFloor())
			{
				velocity.Y = 0;
				_isJumping = false;
			}
			
		// Add variable for horizontal move.
		velocity.X = 0;
		
		// Get the input direction for rotating
		bool rotateLeft = Input.IsActionPressed("ui_rotate_left");
		bool rotateRight = Input.IsActionPressed("ui_rotate_right");

		//Handle rotating to the left side
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
		
		//Handle rotating to the right side
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
		
		//Handle mid-air rotating to the left side
		if (rotateLeft && !IsOnFloor() && !rotateRight)
		{
			Rotation -= RotationSpeed * 1.5f * (float)delta;
		}
		
		//Handle mid-air rotating to the right side
		if (rotateRight && !IsOnFloor() && !rotateLeft)
		{
			Rotation += RotationSpeed * 1.5f * (float)delta;
		}
		
		//Handle auto rotation when no rotate inputs or both rotate inputs are active
		if ((!rotateRight && IsOnFloor() && !rotateLeft) | (rotateRight && IsOnFloor() && rotateLeft))
		{
			
			if ( -PI/4 < Rotation && Rotation < 0 ){
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
		
		//Only when on feet
		if (-PI/8 < Rotation && Rotation < PI/8 && !rotateLeft && !rotateRight)
		{
		
			//Handle move.
			float direction = Input.GetAxis("ui_left", "ui_right");
			velocity.X += direction * _speed;
			
			// Handle jump.
			if (Input.IsActionPressed("ui_up") && IsOnFloor())
			{
				_isJumping = true;
				_jumpTimer = 0;
			}
		}
		
		//Handle jumpcut.
		if (Input.IsActionPressed("ui_up") && _isJumping)
			{
				if (_jumpTimer < jumpTime)
				{
					velocity.Y = -Mathf.Lerp(_minJumpSpeed, _maxJumpSpeed, _jumpTimer / jumpTime);
					_jumpTimer += (float)delta;
				}
				else
				{
					_isJumping = false;
				}
			}
		else
			{
				_isJumping = false;
			}
			
		//Handle move in mid-air.
		if (!IsOnFloor())
		{
			float direction = Input.GetAxis("ui_left", "ui_right");
			velocity.X += direction * 0.5f * _speed;
		}
		
			
			
			
		
		Velocity = velocity;
		MoveAndSlide();
		
	}

	
}
