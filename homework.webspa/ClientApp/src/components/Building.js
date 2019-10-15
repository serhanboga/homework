import React from 'react';

const Building = ({ building }) => (
    <div className="container">
        <div className="row">
            <i className="material-icons">business</i>
            <div className="col">
                {building.body.name}
            </div>
            <div className="col">
                {building.body.description}
            </div>
            <div className="col">
                {building.body.shortCut}
            </div>
            <small>W:{building.totalWeight.toFixed(2)}</small>
        </div>
    </div>
);


export default Building;  