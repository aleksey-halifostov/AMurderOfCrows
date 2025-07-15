using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MurderOfCrows.RoadBuilding
{
    public class RoadMarksContainer
    {
        private List<Mark> _marks;

        private void UpdateIndexes(int index)
        {
            for (int i = index; i < _marks.Count; i++)
            {
                _marks[i].Index = i;
            }
        }

        public RoadMarksContainer()
        {
            _marks = new List<Mark>();
        }

        public void AddMark(Mark mark)
        {
            _marks.Add(mark);
        }

        public void RemoveMark(int index)
        {
            _marks.RemoveAt(index);
            UpdateIndexes(index);
        }

        public void SetupMarks()
        {
            foreach (Mark mark in _marks)
            {
                mark.DisableCollider();
            }
        }

        public void ClearMarks()
        {
            foreach(Mark mark in _marks) { GameObject.Destroy(mark.gameObject); }
        }
    }
}