using Godot;
using System;

public partial class Character : CharacterBody2D
{
	private const float _speed = 100.0f;
	private const float _jumpSpeed = -400.0f;
	private const float RotationSpeed = 3f;
	private const float Deceleration = 0.5f;

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

		if (rotateLeft)
		{
			Rotation -= RotationSpeed * (float)delta;
		}

		if (rotateRight)
		{
			Rotation += RotationSpeed * (float)delta;
		}
		if (rotateLeft || rotateRight)
		{
			// Apply forward movement based on rotation
			velocity = new Vector2(0, 0).Rotated(Rotation) * _speed;
		}
		else
		{
			// Decelerate if no rotation keys are pressed
			velocity = velocity.Lerp(Vector2.Zero, Deceleration * (float)delta);
		}
		
		Velocity = velocity;
		MoveAndSlide();
		
	}

	
}
