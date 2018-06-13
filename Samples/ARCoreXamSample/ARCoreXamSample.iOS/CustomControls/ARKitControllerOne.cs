﻿using System;
using System.Linq;
using ARKit;
using CoreGraphics;
using Foundation;
using OpenTK;
using SceneKit;
using UIKit;
using Xamarin.Forms;

namespace ARCoreXamSample.iOS.CustomControls
{
    public class ARKitControllerOne : UIViewController
    {
        ARSCNView scnView;
        public ARKitControllerOne() : base() { }

        public override bool ShouldAutorotate() => true;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            scnView = new ARSCNView()
            {
                Frame = this.View.Frame,
                Delegate = new ARDelegate(),
                DebugOptions = ARSCNDebugOptions.ShowFeaturePoints | ARSCNDebugOptions.ShowWorldOrigin,
                UserInteractionEnabled = true
            };

            this.View.AddSubview(scnView);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // Configure ARKit  
            var config = new ARWorldTrackingConfiguration();
            config.PlaneDetection = ARPlaneDetection.Horizontal;

            // This method is called subsequent to ViewDidLoad so we know scnView is instantiated   
            scnView.Session.Run(config, ARSessionRunOptions.RemoveExistingAnchors);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var loc = touch.LocationInView(scnView);
                var worldPos = WorldPositionFromHitTest(loc);
                if (worldPos != null)
                {
                    PlaceCube(worldPos.Item1);
                }
            }
        }
        private SCNVector3 PositionFromTransform(NMatrix4 xform)
        {
            return new SCNVector3(xform.M14, xform.M24, xform.M34);
        }

        (SCNVector3, ARAnchor) WorldPositionFromHitTest(CGPoint pt)
        {
            //Hit test against existing anchors 
            var hits = scnView.HitTest(pt, ARHitTestResultType.ExistingPlaneUsingExtent);
            if (hits != null && hits.Length > 0)
            {
                var anchors = hits.Where(r => r.Anchor is ARPlaneAnchor);
                if (anchors.Count() > 0)
                {
                    var first = anchors.First();
                    var pos = PositionFromTransform(first.WorldTransform);
                    return (pos, (ARPlaneAnchor)first.Anchor);
                }
            }

            return null;
        }

        private SCNMaterial[] LoadMaterials()
        {
            Func<string, SCNMaterial> LoadMaterial = fname =>
            {
                var mat = new SCNMaterial();
                mat.Diffuse.Contents = UIImage.FromFile(fname);
                mat.LocksAmbientWithDiffuse = true; return mat;
            };

            var a = LoadMaterial("msft_logo.png");
            var b = LoadMaterial("xamagon.png");
            var c = LoadMaterial("fsharp.png");

            // This demo was originally in F# :-)   
            return new[] { a, b, a, b, c, c };
        }

        SCNNode PlaceCube(SCNVector3 pos)
        {
            var box = new SCNBox
            {
                Width = 0.10f,
                Height = 0.10f,
                Length = 0.10f
            };

            var cubeNode = new SCNNode
            {
                Position = pos,
                Geometry = box
            };

            cubeNode.Geometry.Materials = LoadMaterials();
            scnView.Scene.RootNode.AddChildNode(cubeNode);
            return cubeNode;
        }
    }

    public class ARDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor != null && anchor is ARPlaneAnchor)
            {
                PlaceAnchorNode(node, anchor as ARPlaneAnchor);
            }
        }

        void PlaceAnchorNode(SCNNode node, ARPlaneAnchor anchor)
        {
            var plane = SCNPlane.Create(anchor.Extent.X, anchor.Extent.Z);
            plane.FirstMaterial.Diffuse.Contents = UIColor.LightGray;

            var planeNode = SCNNode.FromGeometry(plane);

            //Locate the plane at the position of the anchor
            planeNode.Position = new SCNVector3(anchor.Extent.X, 0.0f, anchor.Extent.Z);

            //Rotate it to lie flat
            planeNode.Transform = SCNMatrix4.CreateRotationX((float)(Math.PI / 2.0));
            node.AddChildNode(planeNode);

            //Mark the anchor with a small red box
            var box = new SCNBox
            {
                Height = 0.1f,
                Width = 0.1f,
                Length = 0.1f
            };
            box.FirstMaterial.Diffuse.ContentColor = UIColor.Red;
            var anchorNode = new SCNNode
            {
                Position = new SCNVector3(0, 0, 0),
                Geometry = box
            };
            planeNode.AddChildNode(anchorNode);
        }

        public override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARPlaneAnchor)
            {
                var planeAnchor = anchor as ARPlaneAnchor;
                System.Console.WriteLine($"The (updated) extent of the anchor is [{planeAnchor.Extent.X} , {planeAnchor.Extent.Y} , {planeAnchor.Extent.Z} ]");
            }
        }
    }
}