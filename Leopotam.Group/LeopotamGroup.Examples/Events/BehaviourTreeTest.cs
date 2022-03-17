using Leopotam.Group.Events;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Events {
    public class BehaviourTreeTest : MonoBehaviour {
        /// <summary>
        /// Test data store for pull / push states / sharing data.
        /// </summary>
        class TestDataStore {
            public void Test () {
                Debug.Log ("Api for shared data should be realized there");
            }
        }

        void Start () {
            var bt = new BehaviourTree<TestDataStore> ();

            // BehaviourTreeSequence test.
            bt.GetRootNode ()
                .Then (bt, OnNode1)
                .Then (bt, OnNode2)
                .Then (bt, t => {
                    Debug.Log ("lambda node - ok");
                    return BehaviourTreeResult.Success;
                })

                // Nested BehaviourTreeSequence test.
                .Sequence ().Then (bt, t => {
                    Debug.Log ("internal_sequence1 - ok");
                    return BehaviourTreeResult.Success;
                })
                .Then (bt, t => {
                    Debug.Log ("internal_sequence2 - ok");
                    return BehaviourTreeResult.Success;
                });

            // BehaviourTreeParallel test.
            bt.GetRootNode ()
                .Parallel ()
                .Then (bt, t => {
                    Debug.Log ("parallel_node1 - ok");
                    return BehaviourTreeResult.Success;
                })
                .Then (bt, t => {
                    _pending2++;
                    if (_pending2 < 2) {
                        Debug.Log ("parallel_node2 - pending");
                        return BehaviourTreeResult.Pending;
                    }
                    Debug.Log ("parallel_node2 - ok");
                    return BehaviourTreeResult.Success;
                })

                // BehaviourTreeCondition test.
                .When (bt, t => {
                    _pending3++;
                    return _pending3 >= 2 ? BehaviourTreeResult.Success : BehaviourTreeResult.Fail;
                })
                .Then (bt, t => {
                    Debug.Log ("wow, pending counter >= 2!");
                    return BehaviourTreeResult.Success;
                });

            // BehaviourTreeSelector test.
            bt.GetRootNode ()
                .Select ()
                .Then (bt, t => {
                    Debug.Log ("will be processed");
                    return BehaviourTreeResult.Fail;
                })
                .Then (bt, t => {
                    Debug.Log ("will be processed too");
                    return BehaviourTreeResult.Fail;
                })
                .Then (bt, t => {
                    Debug.Log ("will be processed and stopped on it");
                    return BehaviourTreeResult.Success;
                })
                .Then (bt, t => {
                    Debug.Log ("will not be processed because previous node returned positive result");
                    return BehaviourTreeResult.Fail;
                });

            // behaviour tree ready to process.

            BehaviourTreeResult res;
            do {
                res = bt.Process ();
                Debug.Log (">>> " + res);
            } while (res == BehaviourTreeResult.Pending);
        }

        int _pending;

        int _pending2;

        int _pending3;

        BehaviourTreeResult OnNode1 (BehaviourTree<TestDataStore> bt) {
            _pending++;
            if (_pending < 2) {
                Debug.Log ("node1 - pending");
                return BehaviourTreeResult.Pending;
            }
            Debug.Log ("node1 - ok");
            return BehaviourTreeResult.Success;
        }

        BehaviourTreeResult OnNode2 (BehaviourTree<TestDataStore> bt) {
            // calling store api.
            bt.GetStore ().Test ();

            Debug.Log ("node2 - ok");
            return BehaviourTreeResult.Success;
        }
    }
}