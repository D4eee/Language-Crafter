# Language Crafter

Language Crafter is a Unity narrative-game project for macOS. The repository contains the editable Unity source project; ready-to-run builds are published separately under **Releases**.

[![Unity](https://img.shields.io/badge/Unity-6000.4.4f1-black?logo=unity)](https://unity.com/releases/editor/archive)
[![Platform](https://img.shields.io/badge/platform-macOS-lightgrey?logo=apple)](https://github.com/D4eee/Language-Crafter/releases)

## Download

Download the latest macOS build from the [Releases page](https://github.com/D4eee/Language-Crafter/releases/latest). Unzip `LanguageCrafter-macOS.zip`, then open `LanguageCrafter.app`.

> macOS may ask you to confirm before opening an app downloaded from the internet. Use **System Settings → Privacy & Security → Open Anyway** if necessary.

## Open the project

1. Install Unity `6000.4.4f1` through Unity Hub.
2. Clone this repository.
3. In Unity Hub, choose **Add project from disk** and select the repository folder.
4. Allow Unity to restore the packages and import the assets.

## Repository structure

```text
Assets/          Game scenes, scripts, art, settings, and other source assets
Packages/        Unity package manifest and lock file
ProjectSettings/ Unity editor and build settings
```

Generated folders such as `Library/`, `Temp/`, `Logs/`, `UserSettings/`, and compiled application bundles are intentionally excluded from source control.

## Build for macOS

Open the project in Unity, select **File → Build Profiles**, choose macOS, and build the application. Public downloadable builds belong on the GitHub Releases page rather than inside the Git repository.

## Current version

- App release: `v1.0.0`
- Unity editor: `6000.4.4f1`
- Minimum macOS version: `12.0`

