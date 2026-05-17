# Core Breach — CENG 454 HW3

Pattern-driven systems prototype for CENG 454 Game Programming, Spring 2025-2026.

- **Student:** Taha Kocak — 220444013
- **Engine:** Unity 6 (2D, top-down)
- **Spec:** see [`docs/CENG454_HW3_CoreBreachPatterns.pdf`](docs/CENG454_HW3_CoreBreachPatterns.pdf)

## What it is

A short defend-the-core arena prototype. The player moves a defender around a small map, shoots
projectiles toward the mouse cursor, and protects a central energy core from successive waves of
enemies. The run lasts roughly three minutes. Surviving the final wave wins; the core reaching zero
HP loses.

## Controls

| Action | Input |
|--------|-------|
| Move | `W A S D` |
| Aim | Mouse |
| Shoot | Left Mouse Button |
| Pick up upgrade | `E` |

## Architecture at a glance

- **Observer** — `CoreHealth.OnDamaged` and `EnemyRegistry.OnEnemyKilled` drive HUD, audio, VFX, and
  game-state checks independently.
- **Strategy** — `IMovementStrategy` swaps enemy pathing (`ChaseStrategy`, `ZigZagStrategy`) without
  touching the enemy controller.
- **Object Pool** — projectiles, enemies, and hit effects all reuse pooled instances via
  `IPoolable.OnSpawn` / `OnDespawn`.
- **Decorator** — `IWeapon` is wrapped at runtime by `DamageUpDecorator`, `RapidFireDecorator`, and
  `MultiShotDecorator` when the player picks up an upgrade.
- **Interfaces** — `IDamageable`, `IPoolable`, `IMovementStrategy`, `IWeapon` keep concrete classes
  decoupled.

Full pattern map: [`docs/PATTERN_MAP.md`](docs/PATTERN_MAP.md).

## Project layout

```
CENG454-HW3-Taha-Kocak-220444013/   <- Unity project root
  Assets/_Project/
    Scripts/
      Core/         Interfaces/   Player/      Enemies/
      Weapons/      Pooling/      Waves/       Pickups/
      UI/           Audio/
    Prefabs/
    Scenes/MainScene.unity
docs/   <- spec, planning notes
```

## Build / run

Open the inner `CENG454-HW3-Taha-Kocak-220444013/` folder in Unity 6, open `Assets/_Project/Scenes/MainScene.unity`, press Play.
