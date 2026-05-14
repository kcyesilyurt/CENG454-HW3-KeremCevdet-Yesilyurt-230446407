# Core Breach - CENG454 HW3

Core Breach is a small 3D defend-the-core prototype built in Unity 6.

## Gameplay Overview

The player controls a defender in a top-down arena. The goal is to protect the Energy Core from incoming enemies. Enemies spawn in waves and move toward the core. The player can move and shoot pooled projectiles to destroy enemies.

## Win / Lose Conditions

- Win: Survive and clear 3 enemy waves.
- Lose: The Energy Core health reaches 0.

## Controls

- WASD / Arrow Keys: Move
- Space: Fire

## Architecture Summary

The project avoids a giant GameManager by splitting responsibilities across focused systems:

- `CoreHealth`: Owns Energy Core health and publishes health/destroyed events.
- `HUDController`: Observes health and wave changes and updates UI.
- `GameStateController`: Observes core destruction and wave completion.
- `ProjectilePool`: Reuses projectile instances instead of repeated instantiate/destroy.
- `PlayerWeapon`: Uses the `IWeapon` interface and weapon decorators.
- `EnemyController`: Uses `IMovementStrategy` for enemy movement behavior.
- `WaveManager`: Spawns waves and tracks enemy deaths.

## Patterns Used

| Pattern / Technique | Classes / Interfaces | Purpose |
|---|---|---|
| Interfaces | `IDamageable`, `IPoolable`, `IMovementStrategy`, `IWeapon` | Define behavior boundaries and reduce concrete dependencies. |
| Observer | `CoreHealth`, `HUDController`, `GameStateController`, `WaveManager` | Notify independent systems about health, core destruction, wave change, and win events. |
| Strategy | `IMovementStrategy`, `DirectCoreChaseStrategy`, `ZigZagCoreApproachStrategy`, `EnemyController` | Allow enemies to use interchangeable movement behaviors. |
| Object Pool | `ProjectilePool`, `Projectile`, `IPoolable` | Reuse projectiles safely instead of constantly instantiating and destroying them. |
| Decorator | `IWeapon`, `BaseWeapon`, `WeaponDecorator`, `DamageBoostDecorator`, `FireRateDecorator` | Extend weapon damage and fire rate without modifying the base weapon class. |

## Repository Workflow

Development was organized through feature branches and pull requests:

- `feature/project-setup`
- `feature/player-movement`
- `feature/core-health-observer`
- `feature/projectile-pool-weapon`
- `feature/enemy-strategy`
- `feature/wave-system`
- `feature/decorator-upgrade`
- `feature/final-polish`