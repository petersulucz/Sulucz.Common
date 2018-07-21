// <copyright file="ExpandableGrid.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures
{
    using System;
    using System.Collections.Generic;

    public class ExpandableGrid<T>
    {
        /// <summary>
        /// The dictionary.
        /// </summary>
        private readonly Dictionary<int, Dictionary<int, T>> rows;

        public ExpandableGrid()
        {
            this.rows = new Dictionary<int, Dictionary<int, T>>();
        }

        /// <summary>
        /// Gets the actual count of items in the matrix.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the minimum row in the matrix.
        /// </summary>
        public int MinRow { get; private set; }

        /// <summary>
        /// Gets the maximum row in the matrix.
        /// </summary>
        public int MaxRow { get; private set; }

        /// <summary>
        /// Gets the minimum column in the matrix.
        /// </summary>
        public int MinColumn { get; private set; }

        /// <summary>
        /// Gets the maximum column in the matrix.
        /// </summary>
        public int MaxColumn { get; private set; }

        /// <summary>
        /// Gets or sets the value at a certain index.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The value at the indicies.</returns>
        public T this[int row, int column]
        {
            get
            {
                if (true == this.TryGetValue(row, column, out var value))
                {
                    return value;
                }

                throw new KeyNotFoundException($"The item was not found at row = {row} column = {column}.");
            }

            set
            {
                Dictionary<int, T> columns;
                if (false == this.rows.TryGetValue(row, out columns))
                {
                    this.rows[row] = columns = new Dictionary<int, T>();
                }

                columns[column] = value;

                // Update all variables
                this.MaxColumn = Math.Max(this.MaxColumn, column);
                this.MinColumn = Math.Min(this.MinColumn, column);
                this.MaxRow = Math.Max(this.MaxRow, row);
                this.MinRow = Math.Min(this.MinRow, row);
            }
        }

        public bool TryGetValue(int row, int column, out T value)
        {
            Dictionary<int, T> columns;
            if (true == this.rows.TryGetValue(row, out columns)
                && true == columns.TryGetValue(column, out value))
            {
                return true;
            }

            value = default(T);
            return false;
        }
    }
}
