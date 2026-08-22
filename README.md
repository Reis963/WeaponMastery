# Weapon Mastery

A BepInEx plugin for SPT *(Single Player Tarkov)* that adds configurable
variation to weapon-mastery and magazine-check animation parameters.

## Installation

1. Download the latest version of the plugin from the releases page.
2. Extract the zip file to the root of your SPT installation.

## Configuration

The plugin creates `BepInEx\config\com.reis963.weaponmastery.cfg` after it is
loaded for the first time.

* `General.Enabled` (default: `true`) enables or disables all animation
  randomization performed by the plugin.
* `Magazine Check.Variation chance` (default: `100`, range: `0` to `100`)
  controls the chance of substituting a lower `AmmoInMag` value when a magazine
  check starts. The weapon's Animator Controller still decides which animation
  that value selects.
* `Weapon Mastery.Variation chance` (default: `100`, range: `0` to `100`)
  controls the chance of replacing Tarkov's native `WeaponLevel` with a random
  level from `0` to `2`. Tarkov normally synchronizes this parameter when
  equipping a weapon, when mastery changes, and in some special reload flows.

## Known Issues

* some guns that use a cylinder as a magazine (MTs-255-12 12ga shotgun, RSh-12 12.7x55 revolver, etc.), some times don't show the round when reloading.

## License

The plugin is licensed under the MIT License. See the LICENSE file for more information.
