using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using Fluxor;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;

namespace Luthetus.Common.RazorLib.Store.TreeViewCase;

public partial class TreeViewStateContainer
{
    private class Reducer
    {
        [ReducerMethod]
        public static TreeViewStateContainer ReduceRegisterTreeViewStateAction(
            TreeViewStateContainer inTreeViewStateContainer,
            RegisterTreeViewStateAction registerTreeViewStateAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == registerTreeViewStateAction.TreeViewState.TreeViewStateKey);
            
            if (existingTreeViewState is not null)
                return inTreeViewStateContainer;

            var nextList = inTreeViewStateContainer.TreeViewStatesList
                .Add(registerTreeViewStateAction.TreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceDisposeTreeViewStateAction(
            TreeViewStateContainer inTreeViewStateContainer,
            DisposeTreeViewStateAction disposeTreeViewStateAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == disposeTreeViewStateAction.TreeViewStateKey);

            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList
                .Remove(existingTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceWithRootAction(
            TreeViewStateContainer inTreeViewStateContainer,
            WithRootAction withRootAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == withRootAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;

            var nextTreeViewState = existingTreeViewState with
            {
                RootNode = withRootAction.TreeViewNoType,
                ActiveNode = withRootAction.TreeViewNoType
            };

            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                        existingTreeViewState,
                        nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceAddChildNodeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            AddChildNodeAction addChildNodeAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == addChildNodeAction.TreeViewStateKey);

            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            var parent = addChildNodeAction.Parent;
            var child = addChildNodeAction.Child;

            child.Parent = parent;
            child.IndexAmongSiblings = parent.Children.Count;
            child.TreeViewChangedKey = TreeViewChangedKey
                .NewTreeViewChangedKey();
            
            parent.Children.Add(child);

            var rerenderNodeAction = new ReRenderSpecifiedNodeAction(
                addChildNodeAction.TreeViewStateKey, 
                parent);
            
            return ReduceReRenderNodeAction(
                inTreeViewStateContainer,
                rerenderNodeAction);
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceReRenderNodeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            ReRenderSpecifiedNodeAction reRenderSpecifiedNodeAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == reRenderSpecifiedNodeAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            PerformMarkForRerender(reRenderSpecifiedNodeAction.Node);
            
            var nextTreeViewState = existingTreeViewState with
            {
                StateId = Guid.NewGuid()
            };
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                existingTreeViewState, 
                nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceSetActiveNodeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            SetActiveNodeAction setActiveNodeAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == setActiveNodeAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            if (existingTreeViewState.ActiveNode is not null)
                PerformMarkForRerender(existingTreeViewState.ActiveNode);

            if (setActiveNodeAction.NextActiveNode is not null)
                PerformMarkForRerender(setActiveNodeAction.NextActiveNode);

            var nextTreeViewState = existingTreeViewState with
            {
                ActiveNode = setActiveNodeAction.NextActiveNode
            };
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                        existingTreeViewState, 
                        nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceAddSelectedNodeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            AddSelectedNodeAction addSelectedNodeAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == addSelectedNodeAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            var nextTreeViewState = PerformAddSelectedNode(
                existingTreeViewState,
                addSelectedNodeAction);
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                        existingTreeViewState, 
                        nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceRemoveSelectedNodeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            RemoveSelectedNodeAction removeSelectedNodeAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == removeSelectedNodeAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            var nodeToRemove = existingTreeViewState.SelectedNodes
                .FirstOrDefault(x => 
                    x.TreeViewNodeKey == removeSelectedNodeAction.TreeViewNodeKey);

            if (nodeToRemove is null)
                return inTreeViewStateContainer;
            
            PerformMarkForRerender(nodeToRemove);

            var nextSelectedNodesList = existingTreeViewState.SelectedNodes
                .Remove(nodeToRemove);

            var nextTreeViewState = existingTreeViewState with
            {
                SelectedNodes = nextSelectedNodesList
            };
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                existingTreeViewState, 
                nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceClearSelectedNodesAction(
            TreeViewStateContainer inTreeViewStateContainer,
            ClearSelectedNodesAction clearSelectedNodesAction)
        {
            var existingTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == clearSelectedNodesAction.TreeViewStateKey);
            
            if (existingTreeViewState is null)
                return inTreeViewStateContainer;
            
            var nextTreeViewState = PerformClearSelectedNodes(
                existingTreeViewState);
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                existingTreeViewState, 
                nextTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveLeftAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveLeftAction moveLeftAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveLeftAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            var outTreeViewState = inTreeViewState;
            
            if (moveLeftAction.ShiftKey)
                return inTreeViewStateContainer;

            outTreeViewState = PerformClearSelectedNodes(
                outTreeViewState);
                
            if (outTreeViewState.ActiveNode is null)
                return inTreeViewStateContainer;

            if (outTreeViewState.ActiveNode.IsExpanded && 
                outTreeViewState.ActiveNode.IsExpandable)
            {
                outTreeViewState.ActiveNode.IsExpanded = false;

                var reRenderNodeAction = new ReRenderSpecifiedNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    outTreeViewState.ActiveNode);
                
                return ReduceReRenderNodeAction(
                    inTreeViewStateContainer,
                    reRenderNodeAction);
            }
            
            if (outTreeViewState.ActiveNode.Parent is not null)
            {
                var setActiveNodeAction = new SetActiveNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    outTreeViewState.ActiveNode.Parent);
                
                outTreeViewState = PerformSetActiveNode(
                    outTreeViewState,
                    setActiveNodeAction);
            }

            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveDownAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveDownAction moveDownAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveDownAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            var outTreeViewState = inTreeViewState;

            if (!moveDownAction.ShiftKey)
            {
                outTreeViewState = PerformClearSelectedNodes(
                    outTreeViewState);
                
                if (outTreeViewState.ActiveNode is null)
                    return inTreeViewStateContainer;
            }

            if (outTreeViewState.ActiveNode.IsExpanded &&
                outTreeViewState.ActiveNode.Children.Any())
            {
                var nextActiveNode = outTreeViewState.ActiveNode.Children[0];

                if (moveDownAction.ShiftKey)
                {
                    var addSelectedNodeAction = new AddSelectedNodeAction(
                        moveDownAction.TreeViewStateKey,
                        nextActiveNode);
                
                    outTreeViewState = PerformAddSelectedNode(
                        outTreeViewState,
                        addSelectedNodeAction);
                }
                
                var setActiveNodeAction = new SetActiveNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    nextActiveNode);
                
                outTreeViewState = PerformSetActiveNode(
                    outTreeViewState,
                    setActiveNodeAction);
            }
            else
            {
                var target = outTreeViewState.ActiveNode;
            
                while (target.Parent is not null &&
                       target.IndexAmongSiblings == 
                       target.Parent.Children.Count - 1)
                {
                    target = target.Parent;
                }

                if (target.Parent is null ||
                    target.IndexAmongSiblings == 
                    target.Parent.Children.Count - 1)
                {
                    return inTreeViewStateContainer;
                }

                var nextActiveNode = target.Parent.Children[
                    target.IndexAmongSiblings + 
                    1];

                if (moveDownAction.ShiftKey)
                {
                    var addSelectedNodeAction = new AddSelectedNodeAction(
                        moveDownAction.TreeViewStateKey,
                        nextActiveNode);
                
                    outTreeViewState = PerformAddSelectedNode(
                        outTreeViewState,
                        addSelectedNodeAction);
                }
                
                var setActiveNodeAction = new SetActiveNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    nextActiveNode);

                outTreeViewState = PerformSetActiveNode(
                    outTreeViewState,
                    setActiveNodeAction);
            }
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveUpAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveUpAction moveUpAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveUpAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }

            var outTreeViewState = inTreeViewState;

            if (outTreeViewState.ActiveNode.Parent is null)
                return inTreeViewStateContainer;
            
            if (!moveUpAction.ShiftKey)
            {
                outTreeViewState = PerformClearSelectedNodes(
                    outTreeViewState);
                
                if (outTreeViewState.ActiveNode is null)
                    return inTreeViewStateContainer;
            }

            if (outTreeViewState.ActiveNode.IndexAmongSiblings == 0)
            {
                var nextActiveNode = outTreeViewState.ActiveNode.Parent!;
            
                if (moveUpAction.ShiftKey)
                {
                    var addSelectedNodeAction = new AddSelectedNodeAction(
                        moveUpAction.TreeViewStateKey,
                        nextActiveNode);
                
                    outTreeViewState = PerformAddSelectedNode(
                        outTreeViewState,
                        addSelectedNodeAction);
                }
                
                var setActiveNodeAction = new SetActiveNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    outTreeViewState.ActiveNode!.Parent);

                outTreeViewState = PerformSetActiveNode(
                    outTreeViewState,
                    setActiveNodeAction);
            }
            else
            {
                var target = outTreeViewState.ActiveNode.Parent.Children[
                        outTreeViewState.ActiveNode.IndexAmongSiblings - 1];

                while (true)
                {
                    if (moveUpAction.ShiftKey)
                    {
                        var addSelectedNodeAction = new AddSelectedNodeAction(
                            moveUpAction.TreeViewStateKey,
                            target);
                
                        outTreeViewState = PerformAddSelectedNode(
                            outTreeViewState,
                            addSelectedNodeAction);
                    }
                    
                    if (target.IsExpanded &&
                        target.Children.Any())
                    {
                        target = target.Children.Last();
                    }
                    else
                    {
                        break;
                    }
                }

                var setActiveNodeAction = new SetActiveNodeAction(
                    outTreeViewState.TreeViewStateKey,
                    target);

                outTreeViewState = PerformSetActiveNode(
                    outTreeViewState,
                    setActiveNodeAction);
            }
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveRightAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveRightAction moveRightAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveRightAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            var outTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveRightAction.TreeViewStateKey);
            
            if (outTreeViewState is null ||
                outTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            if (moveRightAction.ShiftKey)
                return inTreeViewStateContainer;
            
            outTreeViewState = PerformClearSelectedNodes(
                outTreeViewState);
            
            if (outTreeViewState.ActiveNode is null)
                return inTreeViewStateContainer;

            if (outTreeViewState.ActiveNode.IsExpanded)
            {
                if (outTreeViewState.ActiveNode.Children.Any())
                {
                    var setActiveNodeAction = new SetActiveNodeAction(
                        outTreeViewState.TreeViewStateKey,
                        outTreeViewState.ActiveNode.Children[0]);

                    outTreeViewState = PerformSetActiveNode(
                        outTreeViewState,
                        setActiveNodeAction);
                }
            }
            else if (outTreeViewState.ActiveNode.IsExpandable)
            {
                outTreeViewState.ActiveNode.IsExpanded = true;

                moveRightAction.LoadChildrenAction
                    .Invoke(outTreeViewState.ActiveNode);
            }
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveHomeAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveHomeAction moveHomeAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveHomeAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            var outTreeViewState = inTreeViewState;
            
            if (!moveHomeAction.ShiftKey)
            {
                outTreeViewState = PerformClearSelectedNodes(
                    outTreeViewState);
                
                if (outTreeViewState.ActiveNode is null)
                    return inTreeViewStateContainer;
            }

            TreeViewNoType target;
            
            if (outTreeViewState.RootNode is TreeViewAdhoc)
            {
                if (outTreeViewState.RootNode.Children.Any())
                    target = outTreeViewState.RootNode.Children[0];
                else
                    target = outTreeViewState.RootNode;
            }
            else
            {
                target = outTreeViewState.RootNode;
            }
            
            if (moveHomeAction.ShiftKey)
            {
                var addSelectedNodeAction = new AddSelectedNodeAction(
                    moveHomeAction.TreeViewStateKey,
                    target);
                
                outTreeViewState = PerformAddSelectedNode(
                    outTreeViewState,
                    addSelectedNodeAction);
            }
            
            var setActiveNodeAction = new SetActiveNodeAction(
                outTreeViewState.TreeViewStateKey,
                target);

            outTreeViewState = PerformSetActiveNode(
                outTreeViewState,
                setActiveNodeAction);
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        [ReducerMethod]
        public static TreeViewStateContainer ReduceMoveEndAction(
            TreeViewStateContainer inTreeViewStateContainer,
            MoveEndAction moveEndAction)
        {
            var inTreeViewState = inTreeViewStateContainer.TreeViewStatesList
                .FirstOrDefault(x =>
                    x.TreeViewStateKey == moveEndAction.TreeViewStateKey);
            
            if (inTreeViewState is null ||
                inTreeViewState.ActiveNode is null)
            {
                return inTreeViewStateContainer;
            }
            
            var outTreeViewState = inTreeViewState;
            
            if (!moveEndAction.ShiftKey)
            {
                outTreeViewState = PerformClearSelectedNodes(
                    outTreeViewState);
                
                if (outTreeViewState.ActiveNode is null)
                    return inTreeViewStateContainer;
            }

            var target = outTreeViewState.RootNode;
            
            while (target.IsExpanded &&
                   target.Children.Any())
            {
                target = target.Children.Last();
            }

            if (moveEndAction.ShiftKey)
            {
                var addSelectedNodeAction = new AddSelectedNodeAction(
                    moveEndAction.TreeViewStateKey,
                    target);
                
                outTreeViewState = PerformAddSelectedNode(
                    outTreeViewState,
                    addSelectedNodeAction);
            }
            
            var setActiveNodeAction = new SetActiveNodeAction(
                outTreeViewState.TreeViewStateKey,
                target);

            outTreeViewState = PerformSetActiveNode(
                outTreeViewState,
                setActiveNodeAction);
            
            var nextList = inTreeViewStateContainer.TreeViewStatesList.Replace(
                inTreeViewState, 
                outTreeViewState);

            return new TreeViewStateContainer
            {
                TreeViewStatesList = nextList
            };
        }
        
        /// <summary>
        /// Mutates state, does not return anything.
        /// </summary>
        private static void PerformMarkForRerender(
            TreeViewNoType treeViewNoType)
        {
            var markForRerenderTarget = treeViewNoType;
            
            while (markForRerenderTarget is not null)
            {
                markForRerenderTarget.TreeViewChangedKey = TreeViewChangedKey
                    .NewTreeViewChangedKey();

                markForRerenderTarget = markForRerenderTarget.Parent;
            }
        }
        
        [Pure]
        private static TreeViewState PerformAddSelectedNode(
            TreeViewState treeViewState,
            AddSelectedNodeAction addSelectedNodeAction)
        {
            if (treeViewState.ActiveNode is null)
                return treeViewState;
            
            var selectedNodes = treeViewState.SelectedNodes;

            if (!selectedNodes.Any())
            {
                selectedNodes = new TreeViewNoType[]
                {
                    treeViewState.ActiveNode
                }.ToImmutableList();
            }

            if (selectedNodes.Any(x => 
                    x.TreeViewNodeKey == addSelectedNodeAction.NodeSelection.TreeViewNodeKey))
            {
                return treeViewState;
            }
            
            var nextSelectedNodesList = selectedNodes
                .Add(addSelectedNodeAction.NodeSelection);
            
            PerformMarkForRerender(addSelectedNodeAction.NodeSelection);
            PerformMarkForRerender(treeViewState.ActiveNode);

            return treeViewState with
            {
                SelectedNodes = nextSelectedNodesList
            };
        }
        
        [Pure]
        private static TreeViewState PerformSetActiveNode(
            TreeViewState inTreeViewState,
            SetActiveNodeAction setActiveNodeAction)
        {
            if (inTreeViewState.ActiveNode is not null)
                PerformMarkForRerender(inTreeViewState.ActiveNode);

            if (setActiveNodeAction.NextActiveNode is not null)
                PerformMarkForRerender(setActiveNodeAction.NextActiveNode);

            return inTreeViewState with
            {
                ActiveNode = setActiveNodeAction.NextActiveNode
            };
        }
        
        [Pure]
        private static TreeViewState PerformClearSelectedNodes(
            TreeViewState inTreeViewState)
        {
            if (!inTreeViewState.SelectedNodes.Any())
                return inTreeViewState;
            
            foreach (var node in inTreeViewState.SelectedNodes)
            {
                PerformMarkForRerender(node);
            }

            var nextSelectedNodes = inTreeViewState.ActiveNode is null 
                ? ImmutableList<TreeViewNoType>.Empty
                : new TreeViewNoType[] { inTreeViewState.ActiveNode }
                    .ToImmutableList();
            
            return inTreeViewState with
            {
                SelectedNodes = nextSelectedNodes
            };
        }
    }
}