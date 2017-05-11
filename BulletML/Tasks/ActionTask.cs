using System;
using BulletML.Enums;
using BulletML.Nodes;
using System.Diagnostics;
using System.IO;

namespace BulletML.Tasks
{
    /// <summary>
    /// An action task, this element contains a list of tasks that are repeated.
    /// </summary>
    public class ActionTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The max number of times to repeat this action
        /// </summary>
        public int RepeatNumMax { get; private set; }

        /// <summary>
        /// The number of times this task has been run.
        /// This starts at 0 and the task will repeat until it hits the "max"
        /// </summary>
        public int RepeatNum { get; private set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ActionTask(ActionNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        /// <summary>
        /// Parse a specified node and bullet into this task.
        /// </summary>
        /// <param name="bullet">The bullet this action is controlling.</param>
        public override void ParseTasks(Bullet bullet)
        {
            // Set the number of times to repeat this action
            var actionNode = Node as ActionNode;

            Debug.Assert(null != actionNode);

            RepeatNumMax = actionNode.RepeatNum(this, bullet);

            // Check that there is a wait node if the RepeatNumMax is 0 (= infinite)
            if (RepeatNumMax == 0)
            {
                var waitNode = actionNode.ChildNodes.Find(
                    node => node.Name == NodeName.wait && node.GetValue(this) > 0f
                ) as WaitNode;

                if (waitNode == null)
                {
                    if (actionNode.Name == NodeName.actionRef)
                    {
                        Console.WriteLine("Warning: A repeat node is used with an actionRef child, " +
                                          "please make sure that this actionRef contains a wait node " +
                                          "or it will block the program.");
                    }
                    else
                    {
                        throw new InvalidDataException("You have an infinite loop with no wait node, " +
                                                       "this is bad because it will block the program.");
                    }
                }
            }

            // Is this an actionRef task?
            if (Node.Name == NodeName.actionRef)
            {
                // Add a sub task under this one for the referenced action
                var actionRefNode = Node as ActionRefNode;

                // Create the action task
                if (actionRefNode != null)
                {
                    var actionTask = new ActionTask(actionRefNode.ReferencedActionNode, this);

                    // Parse the children of the action node into the task
                    actionTask.ParseTasks(bullet);

                    // Store the task
                    ChildTasks.Add(actionTask);
                }
            }

            // Call the base class
            base.ParseTasks(bullet);
        }

        /// <summary>
        /// This sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The associated bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            RepeatNum = 0;
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override TaskRunStatus Run(Bullet bullet)
        {
            // Run the action until we hit the limit
            while ((RepeatNumMax == 0) || RepeatNum < RepeatNumMax)
            {
                var runStatus = base.Run(bullet);

                // What was the return value from running all the child actions?
                switch (runStatus)
                {
                    case TaskRunStatus.End:
                        {
                            // The actions completed successfully, initialize everything and run it again
                            RepeatNum++;

                            // Reset all the child tasks
                            foreach (var task in ChildTasks)
                                task.InitTask(bullet);
                        }
                        break;

                    case TaskRunStatus.Stop:
                        {
                            // Something in the child tasks paused this action
                            return runStatus;
                        }

                    default:
                        {
                            // One of the child tasks needs to keep running next frame
                            return TaskRunStatus.Continue;
                        }
                }
            }

            // If it gets here, all the child tasks have been run the correct number of times
            Finished = true;
            return TaskRunStatus.End;
        }

        #endregion Methods
    }
}