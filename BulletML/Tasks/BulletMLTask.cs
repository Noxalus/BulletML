using BulletML.Enums;
using BulletML.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This is a task.
    /// Each task is the action from a single XML node, for one bullet.
    /// Basically each bullet makes a tree of these to match its pattern.
    /// </summary>
    public class BulletMLTask
    {
        #region Members

        /// <summary>
        /// A list of child tasks of this task.
        /// </summary>
        public List<BulletMLTask> ChildTasks { get; private set; }

        /// <summary>
        /// The parameter list for this task
        /// </summary>
        public List<float> Params { get; private set; }

        /// <summary>
        /// The parent task of this task in the tree.
        /// Used to fetch param values.
        /// </summary>
        public BulletMLTask Owner { get; set; }

        /// <summary>
        /// The XML node that this task represents.
        /// </summary>
        public BulletMLNode Node { get; private set; }

        /// <summary>
        /// whether or not this task has finished running
        /// </summary>
        public bool Finished { get; protected set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public BulletMLTask(BulletMLNode node, BulletMLTask owner)
        {
            if (node == null)
                throw new NullReferenceException("Node argument cannot be null.");

            Node = node;
            Owner = owner;

            ChildTasks = new List<BulletMLTask>();
            Params = new List<float>();
            Finished = false;
        }

        /// <summary>
        /// Parse a specified node and bullet into this task.
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        public virtual void ParseTasks(Bullet bullet)
        {
            if (bullet == null)
                throw new NullReferenceException("Bullet argument cannot be null");

            foreach (var childNode in Node.ChildNodes)
                ParseChildNode(childNode, bullet);
        }

        /// <summary>
        /// Parse a specified node and bullet into this task.
        /// </summary>
        /// <param name="childNode">The node for this task.</param>
        /// <param name="bullet">The bullet this task is controlling.</param>
        private void ParseChildNode(BulletMLNode childNode, Bullet bullet)
        {
            Debug.Assert(null != childNode);
            Debug.Assert(null != bullet);

            // Construct the correct type of node
            switch (childNode.Name)
            {
                case NodeName.repeat:
                    {
                        // Convert the node to an repeatnode
                        var repeatNode = childNode as RepeatNode;

                        // Create a placeholder BulletMLTask for the repeat node
                        var repeatTask = new RepeatTask(repeatNode, this);

                        // Parse the child nodes into the repeat task
                        repeatTask.ParseTasks(bullet);

                        // Store the task
                        ChildTasks.Add(repeatTask);
                    }
                    break;

                case NodeName.action:
                    {
                        // Convert the node to an ActionNode
                        var actionNode = childNode as ActionNode;

                        // Create the action task
                        var actionTask = new ActionTask(actionNode, this);

                        // Parse the children of the action node into the task
                        actionTask.ParseTasks(bullet);

                        // Store the task
                        ChildTasks.Add(actionTask);
                    }
                    break;

                case NodeName.actionRef:
                    {
                        // Convert the node to an ActionNode
                        var actionRefNode = childNode as ActionRefNode;

                        // Create the action task
                        var actionTask = new ActionTask(actionRefNode, this);

                        // Add the params to the action task
                        for (int i = 0; i < childNode.ChildNodes.Count; i++)
                            actionTask.Params.Add(childNode.ChildNodes[i].GetValue(this));

                        // Parse the children of the action node into the task
                        actionTask.ParseTasks(bullet);

                        // Store the task
                        ChildTasks.Add(actionTask);
                    }
                    break;

                case NodeName.fire:
                    {
                        // Convert the node to a fire node
                        var fireNode = childNode as FireNode;

                        // Create the fire task
                        var fireTask = new FireTask(fireNode, this);

                        // Parse the children of the fire node into the task
                        fireTask.ParseTasks(bullet);
                        // Store the task
                        ChildTasks.Add(fireTask);
                    }
                    break;

                case NodeName.fireRef:
                    {
                        // Convert the node to a fireRef node
                        var fireRefNode = childNode as FireRefNode;

                        // Create the fire task
                        if (fireRefNode != null)
                        {
                            var fireTask = new FireTask(fireRefNode.ReferencedFireNode, this);

                            // Add the params to the fire task
                            for (int i = 0; i < childNode.ChildNodes.Count; i++)
                                fireTask.Params.Add(childNode.ChildNodes[i].GetValue(this));

                            // Parse the children of the action node into the task
                            fireTask.ParseTasks(bullet);

                            // Store the task
                            ChildTasks.Add(fireTask);
                        }
                    }
                    break;

                case NodeName.changeSpeed:
                    {
                        ChildTasks.Add(new ChangeSpeedTask(childNode as ChangeSpeedNode, this));
                    }
                    break;

                case NodeName.changeDirection:
                    {
                        ChildTasks.Add(new ChangeDirectionTask(childNode as ChangeDirectionNode, this));
                    }
                    break;

                case NodeName.wait:
                    {
                        ChildTasks.Add(new WaitTask(childNode as WaitNode, this));
                    }
                    break;

                case NodeName.vanish:
                    {
                        ChildTasks.Add(new VanishTask(childNode as VanishNode, this));
                    }
                    break;

                case NodeName.accel:
                    {
                        ChildTasks.Add(new AccelTask(childNode as AccelNode, this));
                    }
                    break;
                case NodeName.color:
                    {
                        ChildTasks.Add(new ColorTask(childNode as ColorNode, this));
                    }
                    break;
                case NodeName.changeColor:
                    {
                        ChildTasks.Add(new ChangeColorTask(childNode as ChangeColorNode, this));
                    }
                    break;
                case NodeName.changeScale:
                    {
                        ChildTasks.Add(new ChangeScaleTask(childNode as ChangeScaleNode, this));
                    }
                    break;
            }
        }

        /// <summary>
        /// This gets called when nested repeat nodes get initialized.
        /// </summary>
        /// <param name="bullet">bullet.</param>
        public virtual void HardReset(Bullet bullet)
        {
            Finished = false;

            foreach (BulletMLTask task in ChildTasks)
                task.HardReset(bullet);

            SetupTask(bullet);
        }

        /// <summary>
        /// Init this task and all its sub tasks.
        /// This method should be called AFTER the nodes are parsed, but BEFORE run is called.
        /// </summary>
        /// <param name="bullet">the bullet this dude is controlling</param>
        public virtual void InitTask(Bullet bullet)
        {
            Finished = false;

            foreach (BulletMLTask task in ChildTasks)
            {
                task.InitTask(bullet);
            }

            SetupTask(bullet);
        }

        /// <summary>
        /// This sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        protected virtual void SetupTask(Bullet bullet)
        {
            // Overloaded in child classes
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public virtual TaskRunStatus Run(Bullet bullet)
        {
            // Run all the child tasks
            Finished = true;
            for (int i = 0; i < ChildTasks.Count; i++)
            {
                // Is the child task finished running?
                if (!ChildTasks[i].Finished)
                {
                    // Run the child task
                    TaskRunStatus childStaus = ChildTasks[i].Run(bullet);

                    if (childStaus == TaskRunStatus.Stop)
                    {
                        // The child task is paused, so it is not finished
                        Finished = false;
                        return childStaus;
                    }

                    if (childStaus == TaskRunStatus.Continue)
                    {
                        // The child task needs to do some more work
                        Finished = false;
                    }
                }
            }

            return (Finished ? TaskRunStatus.End : TaskRunStatus.Continue);
        }

        /// <summary>
        /// Get the value of a parameter of this task.
        /// </summary>
        /// <returns>The parameter value.</returns>
        /// <param name="iParamNumber">the index of the parameter to get</param>
        public double GetParamValue(int iParamNumber)
        {
            //if that task doesn't have any params, go up until we find one that does
            if (Params.Count < iParamNumber)
            {
                //the current task doens't have enough params to solve this value
                if (null != Owner)
                {
                    return Owner.GetParamValue(iParamNumber);
                }
                else
                {
                    //got to the top of the list...this means not enough params were passed into the ref
                    return 0.0f;
                }
            }

            //the value of that param is the one we want
            return Params[iParamNumber - 1];
        }

        /// <summary>
        /// Gets the node value.
        /// </summary>
        /// <returns>The node value.</returns>
        public float GetNodeValue(Bullet bullet)
        {
            return Node.GetValue(this);
        }

        /// <summary>
        /// Finds the task by label.
        /// This recurses into child tasks to find the taks with the correct label
        /// Used only for unit testing!
        /// </summary>
        /// <returns>The task by label.</returns>
        /// <param name="strLabel">String label.</param>
        public BulletMLTask FindTaskByLabel(string strLabel)
        {
            //check if this is the corretc task
            if (strLabel == Node.Label)
            {
                return this;
            }

            //check if any of teh child tasks have a task with that label
            foreach (BulletMLTask childTask in ChildTasks)
            {
                BulletMLTask foundTask = childTask.FindTaskByLabel(strLabel);
                if (null != foundTask)
                {
                    return foundTask;
                }
            }

            return null;
        }

        /// <summary>
        /// given a label and name, find the task that matches
        /// </summary>
        /// <returns>The task by label and name.</returns>
        /// <param name="strLabel">String label of the task</param>
        /// <param name="name">the name of the node the task should be attached to</param>
        public BulletMLTask FindTaskByLabelAndName(string strLabel, NodeName name)
        {
            //check if this is the corretc task
            if ((strLabel == Node.Label) && (name == Node.Name))
            {
                return this;
            }

            //check if any of teh child tasks have a task with that label
            foreach (BulletMLTask childTask in ChildTasks)
            {
                BulletMLTask foundTask = childTask.FindTaskByLabelAndName(strLabel, name);
                if (null != foundTask)
                {
                    return foundTask;
                }
            }

            return null;
        }

        #endregion Methods
    }
}