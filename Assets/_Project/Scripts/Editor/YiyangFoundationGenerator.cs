using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yiyang.CameraSystem;
using Yiyang.Chapters;
using Yiyang.Core;
using Yiyang.Dialogue;
using Yiyang.Endings;
using Yiyang.Environment;
using Yiyang.Interaction;
using Yiyang.Narration;
using Yiyang.Player;
using Yiyang.SaveLoad;
using Yiyang.SceneManagement;
using Yiyang.UI;

namespace Yiyang.EditorTools
{
    public static class YiyangFoundationGenerator
    {
        private const string Root = "Assets/_Project";
        private static readonly Dictionary<string, Material> Materials = new();
        private static readonly Dictionary<string, MoodProfile> Moods = new();
        private static readonly List<string> ScenePaths = new();

        private struct SceneSpec
        {
            public string Folder;
            public string Name;
            public string Mood;
            public SceneType Type;
            public string Detail;
            public SceneSpec(string folder, string name, string mood, SceneType type, string detail)
            {
                Folder = folder; Name = name; Mood = mood; Type = type; Detail = detail;
            }
        }

        private static readonly SceneSpec[] Scenes =
        {
            new("00_Boot","Boot","Mood_Nightmare_Abstract",SceneType.Real,"Bootstrap scene. Loads the first playable chapter."),
            new("01_Prototype","Prototype_Hallway","Mood_Hospital_NightGreen",SceneType.Investigation,"All core systems in one test hallway."),
            new("Hospital","Hospital_MaternityRoom","Mood_Hospital_ColdDay",SceneType.Memory,"Birth room with bed, curtain, monitor, cold tile."),
            new("Hospital","Hospital_Corridor_Birth","Mood_Hospital_NightGreen",SceneType.Trauma,"Narrow green corridor with flickering fluorescent lights."),
            new("Hospital","Hospital_Day","Mood_Hospital_ColdDay",SceneType.Real,"Day hospital corridor with blue-white institutional light."),
            new("Hospital","Hospital_Night","Mood_Hospital_NightGreen",SceneType.Nightmare,"Night hospital corridor with green shadows."),
            new("Hospital","Hospital_Rain","Mood_Hospital_Rain",SceneType.Memory,"Hospital windows with rain and distant thunder mood."),
            new("Hospital","Hospital_DiagnosisRoom","Mood_Hospital_ColdDay",SceneType.Investigation,"Diagnosis desk, chair, medical chart clue."),
            new("Hospital","Hospital_Ward_Wheelchair","Mood_Hospital_NightGreen",SceneType.Trauma,"Ward with wheelchair receipt clue and empty beds."),
            new("Home","Home_60m2_LivingRoom","Mood_Home_CrampedWarmDark",SceneType.Real,"Cramped living room, sofa, table, family photo clue."),
            new("Home","Home_60m2_ParentsRoom","Mood_Home_CrampedWarmDark",SceneType.Real,"Parents room pressed tightly against hallway."),
            new("Home","Home_60m2_SistersRoom","Mood_Home_CrampedWarmDark",SceneType.Memory,"Small sister room with notebook clue."),
            new("Home","Home_ClueRoom","Mood_Home_CrampedWarmDark",SceneType.Investigation,"Dense clue room for photo and object inspection."),
            new("Home","Home_MemoryRoom","Mood_School_FadedMemory",SceneType.Memory,"Soft uncanny domestic memory."),
            new("School","School_Stairwell","Mood_School_FadedMemory",SceneType.Real,"Long stairwell and concrete landing."),
            new("School","School_Playground","Mood_School_FadedMemory",SceneType.Memory,"Open playground silhouettes in background."),
            new("School","School_EquipmentRoom","Mood_School_Abandoned",SceneType.Investigation,"Cluttered equipment storage with harsh shadows."),
            new("School","School_Toilet","Mood_School_Abandoned",SceneType.Trauma,"Harsh tile room, stalls, bright overhead light."),
            new("School","School_Abandoned","Mood_School_Abandoned",SceneType.Nightmare,"Broken abandoned school hallway."),
            new("School","School_Memory_Classroom","Mood_School_FadedMemory",SceneType.Memory,"Classroom memory with desks and chalkboard."),
            new("School","School_Bullying","Mood_School_Abandoned",SceneType.Trauma,"Shadow figures forming an empty circle."),
            new("School","School_Humiliation","Mood_School_Abandoned",SceneType.Trauma,"Stage-like light and distant silhouettes."),
            new("School","School_SistersMemory","Mood_School_FadedMemory",SceneType.Memory,"Two desks close together in soft memory light."),
            new("School","School_StudyTogetherMemory","Mood_School_FadedMemory",SceneType.Memory,"Study memory with warm desk lamp."),
            new("AmusementPark","AmusementPark_DayMemory","Mood_AmusementPark_BrokenMemory",SceneType.Memory,"Bright decayed carousel placeholder."),
            new("AmusementPark","AmusementPark_FallIncident","Mood_AmusementPark_BrokenMemory",SceneType.Trauma,"Railings and marked fall point."),
            new("AmusementPark","AmusementPark_AfterFall","Mood_AmusementPark_BrokenMemory",SceneType.Trauma,"Still aftermath space near broken ride."),
            new("Bridge","Bridge_Day_SchoolRoute","Mood_Bridge_NightBlue",SceneType.Real,"Long bridge route with railings and river below."),
            new("Bridge","Bridge_Night_ReturnHome","Mood_Bridge_NightBlue",SceneType.Trauma,"Night return bridge with lamps and ending trigger."),
            new("Bridge","Bridge_Leisure","Mood_Bridge_NightBlue",SceneType.Memory,"Bridge leisure memory with calmer spacing."),
            new("Bridge","Bridge_JumpThought","Mood_Bridge_RainFog",SceneType.Nightmare,"Bridge edge, strong vignette mood, jump thought marker."),
            new("Bridge","Bridge_Rain","Mood_Bridge_RainFog",SceneType.Trauma,"Rain bridge with fog and slick ground."),
            new("Bridge","Bridge_Fog","Mood_Bridge_RainFog",SceneType.Nightmare,"Fog bridge with low visibility."),
            new("PoliceStation","PoliceStation_Hallway","Mood_Police_Interrogation",SceneType.Investigation,"Cold institutional police hallway."),
            new("PoliceStation","PoliceStation_InterrogationRoom","Mood_Police_Interrogation",SceneType.Confrontation,"Table, two chairs, overhead light."),
            new("PoliceStation","PoliceStation_Lobby","Mood_Police_Interrogation",SceneType.Real,"Lobby desk and waiting chairs."),
            new("CityEdge","CityEdge_BrokenAlley","Mood_CityEdge_DirtyYellow",SceneType.Real,"Broken alley with trash silhouettes."),
            new("CityEdge","CityEdge_OldStairwell","Mood_CityEdge_DirtyYellow",SceneType.Real,"Old concrete stairwell."),
            new("CityEdge","CityEdge_BrokenRoom","Mood_CityEdge_DirtyYellow",SceneType.Investigation,"Small broken room with dirty wall light."),
            new("CityEdge","CityEdge_Ditch","Mood_CityEdge_DirtyYellow",SceneType.Trauma,"Ditch and water channel."),
            new("CityEdge","CityEdge_GarbageStation","Mood_CityEdge_DirtyYellow",SceneType.Real,"Garbage station and stacked bags."),
            new("SpecialRooms","LiYuancheng_House","Mood_Captivity_RedBlack",SceneType.Confrontation,"Unsettling domestic interior."),
            new("SpecialRooms","ConfrontationRoom","Mood_Confrontation_WhiteNoise",SceneType.Confrontation,"Centered table, harsh light, darkness around."),
            new("SpecialRooms","CaptivityRoom","Mood_Captivity_RedBlack",SceneType.Trauma,"Locked door, mattress, red-black mood."),
            new("SpecialRooms","FinalRoom_Template","Mood_Nightmare_Abstract",SceneType.Ending,"Abstract ending space template.")
        };

        [MenuItem("Yiyang/Generate Technical Foundation")]
        public static void GenerateFoundation()
        {
            ScenePaths.Clear();
            EnsureFolders();
            CreateMaterials();
            CreateMoods();
            CreateScriptableData();
            CreatePrefabs();
            foreach (SceneSpec spec in Scenes) CreateScene(spec);
            ApplyBuildSettings();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Yiyang technical foundation generated.");
        }

        private static void EnsureFolders()
        {
            string[] paths =
            {
                "Scripts/Core","Scripts/Player","Scripts/Camera","Scripts/Interaction","Scripts/Dialogue","Scripts/Narration","Scripts/SceneManagement","Scripts/Chapters","Scripts/Endings","Scripts/SaveLoad","Scripts/Environment","Scripts/UI","Scripts/Debug","Scripts/Editor",
                "Scenes/00_Boot","Scenes/01_Prototype","Scenes/Hospital","Scenes/Home","Scenes/School","Scenes/AmusementPark","Scenes/Bridge","Scenes/PoliceStation","Scenes/CityEdge","Scenes/SpecialRooms",
                "Prefabs/Player","Prefabs/Camera","Prefabs/UI","Prefabs/Interaction","Prefabs/SceneTransitions","Prefabs/Environment","Prefabs/Lighting","Prefabs/Narrative",
                "ScriptableObjects/Chapters","ScriptableObjects/Scenes","ScriptableObjects/Dialogue","ScriptableObjects/Narration","ScriptableObjects/Endings","ScriptableObjects/MoodProfiles","ScriptableObjects/Clues",
                "Materials/Placeholder","Materials/Mood","Materials/Environment","Art/2D_Foreground","Art/3D_Background","Art/PlaceholderSprites","Audio/Ambience","Audio/SFX","Audio/Music","Settings/URP","Settings/Input"
            };
            foreach (string p in paths)
            {
                string full = $"{Root}/{p}";
                if (!AssetDatabase.IsValidFolder(full)) Directory.CreateDirectory(full);
            }
        }

        private static void CreateMaterials()
        {
            AddMaterial("Mat_Floor_Dark", new Color(0.12f, 0.12f, 0.12f));
            AddMaterial("Mat_Wall_Cold", new Color(0.28f, 0.34f, 0.36f));
            AddMaterial("Mat_Wall_Warm", new Color(0.34f, 0.27f, 0.21f));
            AddMaterial("Mat_RedBlack", new Color(0.18f, 0.03f, 0.03f));
            AddMaterial("Mat_DirtyYellow", new Color(0.42f, 0.35f, 0.15f));
            AddMaterial("Mat_Silhouette", new Color(0.015f, 0.015f, 0.018f));
            AddMaterial("Mat_Label", new Color(0.9f, 0.86f, 0.72f));
            AddMaterial("Mat_Clue", new Color(0.75f, 0.65f, 0.36f));
            AddMaterial("Mat_Transition", new Color(0.05f, 0.08f, 0.12f));
        }

        private static void AddMaterial(string name, Color color)
        {
            string path = $"{Root}/Materials/Placeholder/{name}.mat";
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null)
            {
                mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                AssetDatabase.CreateAsset(mat, path);
            }
            mat.color = color;
            Materials[name] = mat;
        }

        private static void CreateMoods()
        {
            AddMood("Mood_Hospital_ColdDay", new Color(.09f,.12f,.14f), new Color(.18f,.27f,.3f), .018f, new Color(.55f,.75f,.85f), .85f);
            AddMood("Mood_Hospital_NightGreen", new Color(.04f,.08f,.06f), new Color(.04f,.14f,.1f), .045f, new Color(.35f,.8f,.62f), .65f);
            AddMood("Mood_Hospital_Rain", new Color(.05f,.07f,.09f), new Color(.08f,.11f,.16f), .055f, new Color(.45f,.62f,.82f), .55f);
            AddMood("Mood_Home_CrampedWarmDark", new Color(.14f,.09f,.06f), new Color(.16f,.09f,.05f), .035f, new Color(.9f,.55f,.28f), .65f);
            AddMood("Mood_School_FadedMemory", new Color(.12f,.11f,.09f), new Color(.21f,.19f,.14f), .026f, new Color(.85f,.75f,.52f), .75f);
            AddMood("Mood_School_Abandoned", new Color(.05f,.055f,.05f), new Color(.08f,.09f,.08f), .05f, new Color(.55f,.63f,.52f), .45f);
            AddMood("Mood_AmusementPark_BrokenMemory", new Color(.14f,.1f,.12f), new Color(.2f,.09f,.15f), .035f, new Color(.9f,.45f,.65f), .65f);
            AddMood("Mood_Bridge_NightBlue", new Color(.035f,.05f,.09f), new Color(.04f,.06f,.13f), .045f, new Color(.28f,.43f,.9f), .6f);
            AddMood("Mood_Bridge_RainFog", new Color(.035f,.045f,.055f), new Color(.1f,.12f,.15f), .08f, new Color(.35f,.48f,.75f), .45f);
            AddMood("Mood_Police_Interrogation", new Color(.08f,.08f,.09f), new Color(.1f,.1f,.12f), .025f, new Color(.75f,.82f,.9f), .9f);
            AddMood("Mood_CityEdge_DirtyYellow", new Color(.11f,.09f,.04f), new Color(.18f,.13f,.06f), .055f, new Color(.9f,.66f,.22f), .62f);
            AddMood("Mood_Captivity_RedBlack", new Color(.07f,.01f,.01f), new Color(.09f,.01f,.01f), .065f, new Color(.9f,.08f,.04f), .45f);
            AddMood("Mood_Confrontation_WhiteNoise", new Color(.13f,.13f,.13f), new Color(.18f,.18f,.18f), .04f, Color.white, 1.1f);
            AddMood("Mood_Nightmare_Abstract", new Color(.025f,.025f,.03f), new Color(.02f,.02f,.025f), .09f, new Color(.7f,.7f,.85f), .35f);
        }

        private static void AddMood(string id, Color ambient, Color fog, float fogDensity, Color lightColor, float lightIntensity)
        {
            string path = $"{Root}/ScriptableObjects/MoodProfiles/{id}.asset";
            MoodProfile mood = AssetDatabase.LoadAssetAtPath<MoodProfile>(path);
            if (mood == null)
            {
                mood = ScriptableObject.CreateInstance<MoodProfile>();
                AssetDatabase.CreateAsset(mood, path);
            }
            mood.moodID = id;
            mood.ambientColor = ambient;
            mood.fogColor = fog;
            mood.fogDensity = fogDensity;
            mood.mainLightColor = lightColor;
            mood.mainLightIntensity = lightIntensity;
            mood.vignetteIntensity = Mathf.Clamp01(fogDensity * 8f);
            Moods[id] = mood;
            EditorUtility.SetDirty(mood);
        }

        private static void CreateScriptableData()
        {
            string[] clueNames = { "Birth Record", "Family Photo", "Sister Notebook", "School Report", "Medical Diagnosis", "Wheelchair Receipt", "Bridge Memory", "Police Statement", "Broken Toy", "Locked Room Key" };
            string[] clueFlags = { "saw_birth_memory", "found_home_clue_photo", "found_school_bullying_note", "found_school_bullying_note", "diagnosed_osteogenesis_imperfecta", "diagnosed_osteogenesis_imperfecta", "saw_bridge_jump_thought", "entered_police_interrogation", "witnessed_sister_fall", "found_captivity_room" };
            for (int i = 0; i < clueNames.Length; i++)
            {
                ClueData clue = CreateAsset<ClueData>($"{Root}/ScriptableObjects/Clues/Clue_{Sanitize(clueNames[i])}.asset");
                clue.clueID = Sanitize(clueNames[i]).ToLowerInvariant();
                clue.title = clueNames[i];
                clue.description = "Placeholder clue description. Replace with final investigation text.";
                clue.associatedFlag = clueFlags[i];
                EditorUtility.SetDirty(clue);
            }

            foreach (string ending in new[] { "Ending_Truth", "Ending_Silence", "Ending_Escape", "Ending_Revenge", "Ending_Forgiveness", "Ending_BadLoop" })
            {
                EndingData data = CreateAsset<EndingData>($"{Root}/ScriptableObjects/Endings/{ending}.asset");
                data.endingID = ending;
                data.endingName = ending.Replace("_", " ");
                data.endingDescription = "Placeholder ending condition. Tune flags and scores later.";
                data.targetEndingScene = "FinalRoom_Template";
                data.priority = ending switch
                {
                    "Ending_Truth" => 50,
                    "Ending_Forgiveness" => 45,
                    "Ending_Escape" => 40,
                    "Ending_Revenge" => 35,
                    "Ending_Silence" => 30,
                    _ => -100
                };
                if (ending.Contains("Truth")) data.minimumTruthScore = 1;
                if (ending.Contains("Silence")) data.minimumSilenceScore = 1;
                if (ending.Contains("Escape")) data.minimumEscapeScore = 1;
                if (ending.Contains("Revenge")) data.minimumViolenceScore = 1;
                if (ending.Contains("Forgiveness")) data.minimumEmpathyScore = 1;
                EditorUtility.SetDirty(data);
            }

            foreach (SceneSpec spec in Scenes)
            {
                if (spec.Name == "Boot") continue;
                SceneData data = CreateAsset<SceneData>($"{Root}/ScriptableObjects/Scenes/Scene_{spec.Name}.asset");
                data.sceneID = spec.Name;
                data.sceneName = spec.Name.Replace("_", " ");
                data.unitySceneName = spec.Name;
                data.sceneType = spec.Type;
                data.description = spec.Detail;
                data.defaultMood = Moods[spec.Mood];
                EditorUtility.SetDirty(data);
            }
        }

        private static T CreateAsset<T>(string path) where T : ScriptableObject
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, path);
            }
            return asset;
        }

        private static void CreatePrefabs()
        {
            SavePrefab(CreatePlayer(), $"{Root}/Prefabs/Player/PF_Player.prefab");
            SavePrefab(CreateCameraRig(), $"{Root}/Prefabs/Camera/PF_CameraRig.prefab");
            SavePrefab(CreateUIRoot(), $"{Root}/Prefabs/UI/PF_GameplayUI.prefab");
            SavePrefab(CreateTransitionDoor("PF_SceneTransitionDoor", "Prototype_Hallway"), $"{Root}/Prefabs/SceneTransitions/PF_SceneTransitionDoor.prefab");
            SavePrefab(CreateClueObject("PF_InspectableClue"), $"{Root}/Prefabs/Interaction/PF_InspectableClue.prefab");
            SavePrefab(CreateSimpleTrigger<NarrationTrigger>("PF_NarrationTrigger"), $"{Root}/Prefabs/Narrative/PF_NarrationTrigger.prefab");
            SavePrefab(CreateSimpleTrigger<DialogueTrigger>("PF_DialogueTrigger"), $"{Root}/Prefabs/Narrative/PF_DialogueTrigger.prefab");
            SavePrefab(CreateSimpleTrigger<StoryFlagTrigger>("PF_StoryFlagTrigger"), $"{Root}/Prefabs/Interaction/PF_StoryFlagTrigger.prefab");
            SavePrefab(CreateSimpleTrigger<NarrationTrigger>("PF_InvisibleTriggerZone"), $"{Root}/Prefabs/Interaction/PF_InvisibleTriggerZone.prefab");
            SavePrefab(CreateCubePrefab("PF_2D_Foreground_Silhouette", "Mat_Silhouette", new Vector3(1f, 3f, .05f)), $"{Root}/Prefabs/Environment/PF_2D_Foreground_Silhouette.prefab");
            SavePrefab(CreateCubePrefab("PF_2D_Foreground_DustLayer", "Mat_Silhouette", new Vector3(12f, 6f, .02f)), $"{Root}/Prefabs/Environment/PF_2D_Foreground_DustLayer.prefab");
            SavePrefab(CreateCubePrefab("PF_3D_BackgroundWall", "Mat_Wall_Cold", new Vector3(8f, 4f, .3f)), $"{Root}/Prefabs/Environment/PF_3D_BackgroundWall.prefab");
            SavePrefab(CreateCubePrefab("PF_3D_CorridorModule", "Mat_Wall_Cold", new Vector3(10f, 3f, 3f)), $"{Root}/Prefabs/Environment/PF_3D_CorridorModule.prefab");
            SavePrefab(CreateCubePrefab("PF_3D_DoorModule", "Mat_Transition", new Vector3(1.2f, 2.4f, .25f)), $"{Root}/Prefabs/Environment/PF_3D_DoorModule.prefab");
            SavePrefab(CreateCubePrefab("PF_3D_WindowModule", "Mat_Label", new Vector3(1.5f, 1f, .12f)), $"{Root}/Prefabs/Environment/PF_3D_WindowModule.prefab");
            SavePrefab(CreateCubePrefab("PF_3D_StairwellModule", "Mat_Wall_Cold", new Vector3(4f, 2f, 3f)), $"{Root}/Prefabs/Environment/PF_3D_StairwellModule.prefab");
            SavePrefab(CreateFlickeringLight(), $"{Root}/Prefabs/Lighting/PF_FlickeringLight.prefab");
            SavePrefab(CreateCubePrefab("PF_FogVolumePlaceholder", "Mat_Silhouette", new Vector3(8f, 3f, .1f)), $"{Root}/Prefabs/Environment/PF_FogVolumePlaceholder.prefab");
        }

        private static void SavePrefab(GameObject go, string path)
        {
            PrefabUtility.SaveAsPrefabAsset(go, path);
            Object.DestroyImmediate(go);
        }

        private static GameObject CreatePlayer()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = "PF_Player";
            go.tag = "Player";
            go.transform.localScale = new Vector3(.75f, 1f, .75f);
            go.AddComponent<CharacterController>();
            go.AddComponent<PlayerController>();
            SphereCollider interaction = go.AddComponent<SphereCollider>();
            interaction.isTrigger = true;
            interaction.radius = 1.25f;
            go.AddComponent<PlayerInteraction>();
            go.GetComponent<Renderer>().sharedMaterial = Materials["Mat_Label"];
            return go;
        }

        private static GameObject CreateCameraRig()
        {
            GameObject rig = new("PF_CameraRig");
            rig.transform.position = new Vector3(0f, 2.4f, -9f);
            Camera cam = new GameObject("Main Camera").AddComponent<Camera>();
            cam.tag = "MainCamera";
            cam.transform.SetParent(rig.transform);
            cam.transform.localPosition = Vector3.zero;
            cam.orthographic = true;
            cam.orthographicSize = 4.6f;
            rig.AddComponent<CameraFollow2_5D>();
            return rig;
        }

        private static GameObject CreateUIRoot()
        {
            GameObject canvasGO = new("PF_GameplayUI");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGO.AddComponent<GraphicRaycaster>();
            AddPrompt(canvasGO.transform);
            AddNarration(canvasGO.transform);
            AddDialogue(canvasGO.transform);
            AddFade(canvasGO.transform);
            AddDebug(canvasGO.transform);
            canvasGO.AddComponent<PauseMenuUI>();
            canvasGO.AddComponent<ClueLogUI>();
            return canvasGO;
        }

        private static void AddPrompt(Transform parent)
        {
            GameObject panel = Panel(parent, "InteractionPrompt", new Vector2(0.5f, 0.22f), new Vector2(260, 44), new Color(0,0,0,.55f));
            CanvasGroup cg = panel.AddComponent<CanvasGroup>();
            Text text = Text(panel.transform, "PromptText", "Press E", 20, TextAnchor.MiddleCenter);
            InteractionPromptUI ui = panel.AddComponent<InteractionPromptUI>();
            ui.canvasGroup = cg;
            ui.promptText = text;
            cg.alpha = 0f;
        }

        private static void AddNarration(Transform parent)
        {
            GameObject panel = Panel(parent, "NarrationBox", new Vector2(0.5f, 0.08f), new Vector2(760, 110), new Color(0,0,0,.68f));
            CanvasGroup cg = panel.AddComponent<CanvasGroup>();
            Text text = Text(panel.transform, "NarrationText", "", 22, TextAnchor.MiddleCenter);
            NarrationUI ui = panel.AddComponent<NarrationUI>();
            ui.canvasGroup = cg;
            ui.narrationText = text;
        }

        private static void AddDialogue(Transform parent)
        {
            GameObject panel = Panel(parent, "DialogueBox", new Vector2(0.5f, 0.17f), new Vector2(820, 180), new Color(0,0,0,.72f));
            CanvasGroup cg = panel.AddComponent<CanvasGroup>();
            Text speaker = Text(panel.transform, "Speaker", "Speaker", 18, TextAnchor.UpperLeft);
            speaker.rectTransform.anchoredPosition = new Vector2(-360, 55);
            Text line = Text(panel.transform, "Line", "", 21, TextAnchor.MiddleLeft);
            line.rectTransform.sizeDelta = new Vector2(700, 70);
            line.rectTransform.anchoredPosition = new Vector2(0, 15);
            GameObject choices = new("Choices");
            choices.transform.SetParent(panel.transform);
            RectTransform cr = choices.AddComponent<RectTransform>();
            cr.sizeDelta = new Vector2(700, 50);
            cr.anchoredPosition = new Vector2(0, -55);
            HorizontalLayoutGroup layout = choices.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8;
            Button choicePrefab = ButtonPrefab("ChoiceButton");
            choicePrefab.transform.SetParent(panel.transform);
            choicePrefab.gameObject.SetActive(false);
            Button cont = ButtonPrefab("ContinueButton");
            cont.transform.SetParent(panel.transform);
            cont.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, -55);
            DialogueUI ui = panel.AddComponent<DialogueUI>();
            ui.canvasGroup = cg; ui.speakerText = speaker; ui.lineText = line; ui.choicesRoot = choices.transform; ui.choiceButtonPrefab = choicePrefab; ui.continueButton = cont;
        }

        private static void AddFade(Transform parent)
        {
            GameObject imageGO = new("FadeOverlay");
            imageGO.transform.SetParent(parent);
            RectTransform rt = imageGO.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;
            Image image = imageGO.AddComponent<Image>();
            image.color = new Color(0,0,0,0);
            FadeTransitionUI fade = imageGO.AddComponent<FadeTransitionUI>();
            fade.fadeImage = image;
        }

        private static void AddDebug(Transform parent)
        {
            GameObject panel = Panel(parent, "DebugPanel", new Vector2(.13f, .72f), new Vector2(310, 260), new Color(0,0,0,.7f));
            CanvasGroup cg = panel.AddComponent<CanvasGroup>();
            Text text = Text(panel.transform, "DebugText", "", 14, TextAnchor.UpperLeft);
            text.rectTransform.sizeDelta = new Vector2(280, 230);
            DebugTools.DebugGamePanel debug = panel.AddComponent<DebugTools.DebugGamePanel>();
            debug.canvasGroup = cg;
            debug.output = text;
            cg.alpha = 0f;
        }

        private static GameObject Panel(Transform parent, string name, Vector2 anchor, Vector2 size, Color color)
        {
            GameObject go = new(name);
            go.transform.SetParent(parent);
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.anchorMin = anchor; rt.anchorMax = anchor; rt.sizeDelta = size; rt.anchoredPosition = Vector2.zero;
            Image img = go.AddComponent<Image>();
            img.color = color;
            return go;
        }

        private static Text Text(Transform parent, string name, string value, int size, TextAnchor alignment)
        {
            GameObject go = new(name);
            go.transform.SetParent(parent);
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = new Vector2(14, 10); rt.offsetMax = new Vector2(-14, -10);
            Text text = go.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = value;
            text.fontSize = size;
            text.color = new Color(.9f,.88f,.8f);
            text.alignment = alignment;
            return text;
        }

        private static Button ButtonPrefab(string name)
        {
            GameObject go = Panel(null, name, new Vector2(.5f,.5f), new Vector2(180, 36), new Color(.08f,.08f,.08f,.9f));
            Text(go.transform, "Text", "Continue", 16, TextAnchor.MiddleCenter);
            return go.AddComponent<Button>();
        }

        private static GameObject CreateTransitionDoor(string name, string target)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.localScale = new Vector3(1.2f, 2.5f, .25f);
            go.GetComponent<Renderer>().sharedMaterial = Materials["Mat_Transition"];
            BoxCollider c = go.GetComponent<BoxCollider>();
            c.isTrigger = true;
            SceneTransition transition = go.AddComponent<SceneTransition>();
            transition.targetSceneName = target;
            return go;
        }

        private static GameObject CreateClueObject(string name)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.localScale = new Vector3(.45f,.45f,.45f);
            go.GetComponent<Renderer>().sharedMaterial = Materials["Mat_Clue"];
            BoxCollider c = go.GetComponent<BoxCollider>();
            c.isTrigger = true;
            go.AddComponent<InspectableClue>();
            return go;
        }

        private static GameObject CreateSimpleTrigger<T>(string name) where T : Component
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.localScale = new Vector3(1.5f, 2.2f, .5f);
            go.GetComponent<Renderer>().enabled = false;
            go.GetComponent<BoxCollider>().isTrigger = true;
            go.AddComponent<T>();
            return go;
        }

        private static GameObject CreateCubePrefab(string name, string mat, Vector3 scale)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = Materials[mat];
            return go;
        }

        private static GameObject CreateFlickeringLight()
        {
            GameObject go = new("PF_FlickeringLight");
            Light light = go.AddComponent<Light>();
            light.type = LightType.Point;
            light.range = 6f;
            light.intensity = .8f;
            go.AddComponent<FlickeringLight>();
            return go;
        }

        private static void CreateScene(SceneSpec spec)
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            RenderSettings.fog = true;
            GameObject root = new("SCENE_ROOT_" + spec.Name);

            if (spec.Name == "Boot")
            {
                GameObject boot = new("GameBootstrap");
                boot.AddComponent<GameBootstrap>();
                Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/UI/PF_GameplayUI.prefab"));
                SaveScene(spec);
                return;
            }

            GameObject systems = new("Scene Configuration");
            systems.transform.SetParent(root.transform);
            systems.AddComponent<AutoSaveTrigger>();
            SceneMoodApplier mood = systems.AddComponent<SceneMoodApplier>();
            mood.mood = Moods[spec.Mood];

            Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/Player/PF_Player.prefab"), new Vector3(-5.5f, 1.05f, 0), Quaternion.identity).name = "Player";
            GameObject cameraRig = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/Camera/PF_CameraRig.prefab"));
            cameraRig.name = "CameraRig";
            Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/UI/PF_GameplayUI.prefab")).name = "GameplayUI";

            GameObject spawn = new("Spawn_Default");
            spawn.transform.position = new Vector3(-5.5f, 1.05f, 0);
            SceneSpawnPoint sp = spawn.AddComponent<SceneSpawnPoint>();
            sp.spawnPointID = "Default";

            CreateLightRig(spec);
            CreateBlockout(spec);
            CreateClue(spec);
            CreateTransitions(spec);

            GameObject label = new("Scene Label");
            label.transform.position = new Vector3(-5.8f, 3.2f, -0.8f);
            TextMesh mesh = label.AddComponent<TextMesh>();
            mesh.text = spec.Name.Replace("_", " ") + "\n" + spec.Detail;
            mesh.fontSize = 32;
            mesh.characterSize = .08f;
            mesh.anchor = TextAnchor.MiddleLeft;
            mesh.color = new Color(.9f,.86f,.72f);

            SaveScene(spec);
        }

        private static void CreateLightRig(SceneSpec spec)
        {
            MoodProfile mood = Moods[spec.Mood];
            GameObject sun = new("Mood Main Light");
            Light light = sun.AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = mood.mainLightColor;
            light.intensity = mood.mainLightIntensity;
            sun.transform.rotation = Quaternion.Euler(50, -35, 0);
            for (int i = 0; i < 4; i++)
            {
                GameObject l = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/Lighting/PF_FlickeringLight.prefab"), new Vector3(-4 + i * 3f, 2.8f, -1.5f), Quaternion.identity);
                l.name = "Flickering Practical Light " + (i + 1);
            }
        }

        private static void CreateBlockout(SceneSpec spec)
        {
            Material wall = spec.Mood.Contains("Home") ? Materials["Mat_Wall_Warm"] : spec.Mood.Contains("City") ? Materials["Mat_DirtyYellow"] : spec.Mood.Contains("Captivity") ? Materials["Mat_RedBlack"] : Materials["Mat_Wall_Cold"];
            Cube("Ground", new Vector3(0, 0, 0), new Vector3(14, .25f, 3f), Materials["Mat_Floor_Dark"]);
            Cube("Back Wall", new Vector3(0, 2, 1.25f), new Vector3(14, 4, .3f), wall);
            Cube("Ceiling", new Vector3(0, 4.05f, 0), new Vector3(14, .18f, 3f), wall);
            Cube("Foreground Silhouette Left", new Vector3(-6.2f, 1.5f, -1.5f), new Vector3(.8f, 3f, .12f), Materials["Mat_Silhouette"]).AddComponent<ForegroundOccluder>();
            Cube("Foreground Silhouette Right", new Vector3(6.3f, 1.4f, -1.5f), new Vector3(.6f, 2.8f, .12f), Materials["Mat_Silhouette"]).AddComponent<ForegroundOccluder>();

            if (spec.Folder == "Bridge")
            {
                Cube("Bridge Deck", new Vector3(0, .25f, 0), new Vector3(16, .3f, 1.6f), Materials["Mat_Floor_Dark"]);
                Cube("River Far Below", new Vector3(0, -1.2f, 1.8f), new Vector3(16, .08f, 2.5f), Materials["Mat_Transition"]);
                for (int i = 0; i < 8; i++) Cube("Bridge Railing " + i, new Vector3(-6 + i * 1.8f, 1.35f, -.75f), new Vector3(.08f, 1.2f, .08f), Materials["Mat_Label"]);
            }
            else if (spec.Folder == "School")
            {
                for (int i = 0; i < 5; i++) Cube("Desk/Silhouette " + i, new Vector3(-3 + i * 1.5f, .55f, .25f), new Vector3(.7f,.5f,.6f), Materials["Mat_Silhouette"]);
                if (spec.Name.Contains("Stair")) Cube("Stairwell Block", new Vector3(1.5f, 1.1f, .2f), new Vector3(4f, .4f, 1.8f), wall);
            }
            else if (spec.Folder == "Home")
            {
                for (int i = 0; i < 5; i++) Cube("Cramped Furniture " + i, new Vector3(-3 + i * 1.3f, .55f, .2f), new Vector3(.9f,.6f,.8f), Materials["Mat_Wall_Warm"]);
            }
            else if (spec.Folder == "Hospital")
            {
                Cube("Hospital Bed", new Vector3(-1.2f, .65f, .2f), new Vector3(2f,.45f,.8f), Materials["Mat_Label"]);
                Cube("Curtain", new Vector3(1.6f, 1.8f, .15f), new Vector3(.12f,2.4f,.05f), Materials["Mat_Wall_Cold"]);
                Cube("Medical Monitor", new Vector3(2.3f, 1.25f, .2f), new Vector3(.45f,.6f,.25f), Materials["Mat_Transition"]);
            }
            else if (spec.Folder == "PoliceStation" || spec.Name.Contains("Confrontation"))
            {
                Cube("Interrogation Table", new Vector3(0, .65f, .1f), new Vector3(1.8f,.35f,1f), Materials["Mat_Label"]);
                Cube("Chair A", new Vector3(-1.4f, .55f, .1f), new Vector3(.5f,.7f,.5f), Materials["Mat_Silhouette"]);
                Cube("Chair B", new Vector3(1.4f, .55f, .1f), new Vector3(.5f,.7f,.5f), Materials["Mat_Silhouette"]);
            }
            else if (spec.Folder == "AmusementPark")
            {
                Cube("Broken Carousel Base", new Vector3(0, .45f, .3f), new Vector3(2.4f,.35f,2.4f), Materials["Mat_RedBlack"]);
                Cube("Fall Incident Marker", new Vector3(2.4f, .8f, .15f), new Vector3(.3f,1.2f,.3f), Materials["Mat_Label"]);
            }
            else if (spec.Folder == "CityEdge")
            {
                for (int i = 0; i < 5; i++) Cube("Trash Pile " + i, new Vector3(-2.5f + i, .45f, .3f), new Vector3(.8f,.55f,.7f), Materials["Mat_DirtyYellow"]);
            }
            else if (spec.Folder == "SpecialRooms")
            {
                Cube("Locked Door", new Vector3(4.2f, 1.35f, .05f), new Vector3(1.1f,2.4f,.25f), Materials["Mat_Transition"]);
                Cube("Mattress Placeholder", new Vector3(-1.5f, .35f, .2f), new Vector3(1.8f,.25f,.9f), Materials["Mat_RedBlack"]);
            }
        }

        private static GameObject Cube(string name, Vector3 pos, Vector3 scale, Material mat)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = pos;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = mat;
            return go;
        }

        private static void CreateClue(SceneSpec spec)
        {
            GameObject clue = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/Interaction/PF_InspectableClue.prefab"), new Vector3(-1.8f, 1f, -0.5f), Quaternion.identity);
            clue.name = "Inspectable Placeholder Clue";
            InspectableClue ic = clue.GetComponent<InspectableClue>();
            string cluePath = spec.Folder switch
            {
                "Hospital" => $"{Root}/ScriptableObjects/Clues/Clue_MedicalDiagnosis.asset",
                "Home" => $"{Root}/ScriptableObjects/Clues/Clue_FamilyPhoto.asset",
                "School" => $"{Root}/ScriptableObjects/Clues/Clue_SisterNotebook.asset",
                "Bridge" => $"{Root}/ScriptableObjects/Clues/Clue_BridgeMemory.asset",
                "PoliceStation" => $"{Root}/ScriptableObjects/Clues/Clue_PoliceStatement.asset",
                "CityEdge" => $"{Root}/ScriptableObjects/Clues/Clue_BrokenToy.asset",
                "SpecialRooms" => $"{Root}/ScriptableObjects/Clues/Clue_LockedRoomKey.asset",
                _ => $"{Root}/ScriptableObjects/Clues/Clue_BirthRecord.asset"
            };
            ic.clue = AssetDatabase.LoadAssetAtPath<ClueData>(cluePath);
        }

        private static void CreateTransitions(SceneSpec spec)
        {
            string next = NextSceneFor(spec.Name);
            if (!string.IsNullOrEmpty(next))
            {
                GameObject door = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>($"{Root}/Prefabs/SceneTransitions/PF_SceneTransitionDoor.prefab"), new Vector3(5.8f, 1.25f, -0.1f), Quaternion.identity);
                door.name = "Transition To " + next;
                SceneTransition t = door.GetComponent<SceneTransition>();
                t.targetSceneName = next;
                t.targetSpawnPointID = "Default";
            }

            if (spec.Name == "Bridge_Night_ReturnHome")
            {
                GameObject ending = CreateSimpleTrigger<EndingTrigger>("Ending Trigger");
                ending.transform.position = new Vector3(4.2f, 1.1f, 0);
                ending.GetComponent<Renderer>().enabled = true;
                ending.GetComponent<Renderer>().sharedMaterial = Materials["Mat_RedBlack"];
            }
        }

        private static string NextSceneFor(string current)
        {
            return current switch
            {
                "Prototype_Hallway" => "Hospital_Corridor_Birth",
                "Hospital_Corridor_Birth" => "Home_60m2_LivingRoom",
                "Home_60m2_LivingRoom" => "School_Stairwell",
                "School_Stairwell" => "Bridge_Night_ReturnHome",
                "Bridge_Night_ReturnHome" => "FinalRoom_Template",
                _ => ""
            };
        }

        private static void SaveScene(SceneSpec spec)
        {
            string path = $"{Root}/Scenes/{spec.Folder}/{spec.Name}.unity";
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), path);
            ScenePaths.Add(path);
        }

        private static void ApplyBuildSettings()
        {
            List<EditorBuildSettingsScene> buildScenes = new();
            foreach (string path in ScenePaths)
                buildScenes.Add(new EditorBuildSettingsScene(path, true));
            EditorBuildSettings.scenes = buildScenes.ToArray();
        }

        private static string Sanitize(string value) => value.Replace(" ", "").Replace("/", "");
    }
}
