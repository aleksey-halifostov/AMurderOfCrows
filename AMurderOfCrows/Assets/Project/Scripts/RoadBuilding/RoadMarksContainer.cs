using System.Collections.Generic;
using UnityEngine;

namespace MurderOfCrows.RoadBuilding
{
    public class RoadMarksContainer
    {
        private List<Mark> _marks;

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

        public void SetupMarksAndClear()
        {
            foreach (Mark mark in _marks)
            {
                mark.DisableCollider();
            }

            ClearMarks();
        }

        public void ClearMarks()
        {
            _marks.Clear();
        }

        private void UpdateIndexes(int index)
        {
            for (int i = index; i < _marks.Count; i++)
            {
                _marks[i].Index = i;
            }
        }
    }
}