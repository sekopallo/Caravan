﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ProjectCaravan.UI.BaseControls.Grid
{
    public class Grid_Horizontal : MonoBehaviour
    {
        public int Rows;
        public int Columns;

        [SerializeField] protected VerticalLayoutGroup LayoutGroup_Y;
        [SerializeField] protected Transform ContentHost;
        [SerializeField] protected GameObject RowPrefab;

        protected List<Row> lstRows = new List<Row>();

        private void OnValidate()
        {
            if (Rows != lstRows.Count)
                SetupRows();
        }

        protected void SetupRows()
        {
            while (Rows > lstRows.Count)
            {
                var row = GameObject.Instantiate(RowPrefab, ContentHost);
                lstRows.Add(row.GetComponent<Row>());

                AdjustContentHost();
            }

            while (Rows < lstRows.Count)
            {
                GameObject.Destroy(lstRows[lstRows.Count - 1].gameObject);
                lstRows.RemoveAt(lstRows.Count - 1);

                AdjustContentHost();
            }
        }

        private void AdjustContentHost()
        {
            RectTransform contentTransform = ContentHost.GetComponent<RectTransform>();
            var rowsTotalHeight = lstRows.Select(x => x.GetComponent<RectTransform>().rect.height).Sum();
            var spacing = LayoutGroup_Y.spacing * (Rows + 1);
            var totalHeight = rowsTotalHeight + spacing;
            contentTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
        }
    }

}
