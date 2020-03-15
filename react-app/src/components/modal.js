import React from 'react';
import { createPortal } from 'react-dom';

const modalRoot = document.getElementById('modal');
class Modal extends React.Component {
  constructor(props) {
    super(props);
    // We create an element div for this modal
    this.element = document.createElement('div');
  }
  // We append the created div to the div#modal
  componentDidMount() {
    modalRoot.appendChild(this.element);
  }
  /**
    * We remove the created div when this Modal Component is unmounted
    * Used to clean up the memory to avoid memory leak 
    */
  componentWillUnmount() {
    modalRoot.removeChild(this.element);
  }
  render() {
    return createPortal((
      <React.Fragment>
        <div className="modal show d-block" id="myModal">
          <div className="modal-dialog">
            <div className="modal-content">

              <div className="modal-header">
                <h4 className="modal-title">Vehicle Details</h4>
                <button type="button" className="close" data-dismiss="modal" onClick={this.props.onClose}>&times;</button>
              </div>

              <div className="modal-body">
                {this.props.children}
              </div>

            </div>
          </div>
        </div>
        <div className="modal-backdrop show"></div>
      </React.Fragment>
    ), this.element);
  }
}
export default Modal;