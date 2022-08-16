using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
    protected StateBase _currentState; 
    public StateBase CurrentState { get { return _currentState; } }
    private Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();

    void Start()
    {
        _currentState = GetInitialState();
        if (_currentState != null)
            _currentState.OnEnter();
    }

    void Update()
    {
        if (_currentState != null)
            _currentState.OnUpdate();
    }

    public void ChangeState(StateBase newState)
    {
        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    protected virtual StateBase GetInitialState()
    {
        return null;
    }
    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T)))
            return _cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if (component != null)
            _cachedComponents.Add(typeof(T), component);
        return component;
    }
    protected void CacheComponent<T>() where T : Component
    {
        var component = base.GetComponent<T>();
        if (component != null)
            _cachedComponents.Add(typeof(T), component);
    }
}

[CustomEditor(typeof(StateMachineBase), true)]
public class StateMachineBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {

        StateMachineBase stateMachine = (StateMachineBase)target;
        if (stateMachine.CurrentState == null)
            EditorGUILayout.LabelField("Current State : ", "null");
        else
            EditorGUILayout.LabelField("Current State : ", stateMachine.CurrentState.Name);
        DrawDefaultInspector();
        Repaint();
    }
}