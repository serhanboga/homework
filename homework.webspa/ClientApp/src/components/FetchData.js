import React, { Component } from 'react';
import Building from './Building';
import Lock from './Lock';
import Group from './Group';
import Medium from './Medium';

import 'bootstrap/js/dist/collapse';
require('bootstrap/dist/js/bootstrap.min');

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { results: [], loading: false, query: '' };
    }
    static renderComponent(data) {
        switch (data.type) {
            case 0:
                return (<Building building={data} />)
            case 1:
                return (<Lock lock={data} />)
            case 2:
                return (<Group group={data} />)
            case 3:
                return (<Medium medium={data} />)
            default:
                return;
        }
    }

    static renderSearchResults(results) {
        return (
            <ul className="list-group list-group-flush text-left">
                {
                    results.map(result => (
                        <li className="list-group-item bg-light" key={result.body.id}>
                            {FetchData.renderComponent(result)}
                        </li>
                    ))
                }
            </ul>
        );
    }

    getData = () => {
        this.setState({ loading: true });

        fetch(`api/Search/WithText/?text='${this.state.query}`)
            .then(response => response.json())
            .then(data => {

                data.sort((a, b) => (a.totalWeight < b.totalWeight) ? 1 : -1); // sort by search weight descending order

                this.setState({ results: data, loading: false });
            });
    }

    handleInputChange = () => {
        this.setState({
            query: this.search.value
        }, () => {
            if (this.state.query && this.state.query.length > 1) {
                if (this.state.query.length % 2 === 0) {
                    this.getData()
                }
            }
        })
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderSearchResults(this.state.results);

        return (
            <div className="content">
                <h3 className="text-muted text-center mt-5"> Entity Search</h3>
                <br />
                <div className="form-group">
                    <input type="text" onChange={this.handleInputChange} className="form-control mt-2" id="searchKey" aria-describedby="Search" placeholder="Start searching" ref={input => this.search = input} />
                </div>

                <div className="form-group">
                    {contents}
                </div>
            </div>
        );
    }
}
