# 🃏 Crazy 8 — Unity C# Scripts

This repository contains the C# scripts used in a Unity-based **Uno-style card game**. 
The logic handles card play, turn control, wild cards, and custom actions like draw-two and pass.

---

## 📁 Script Files Overview

```plaintext
/Assets/Scripts
├── CardSprite.cs        # Handles visual representation of cards
├── Draw2.cs             # Implements 'Draw Two' card logic
├── Game.cs              # Core game loop and main game state controller
├── Pass.cs              # Logic for 'Pass Turn' cards or actions
├── PlayCard.cs          # Validates and plays selected cards
├── PlayerHand.cs        # Manages the cards held by a player
├── wildClub.cs          # Wild card targeting Clubs
├── wildDiamond.cs       # Wild card targeting Diamonds
├── wildHeart.cs         # Wild card targeting Hearts
├── wildSpade.cs         # Wild card targeting Spades
