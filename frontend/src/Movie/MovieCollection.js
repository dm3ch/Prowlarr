import PropTypes from 'prop-types';
import React, { Component } from 'react';
import MonitorToggleButton from 'Components/MonitorToggleButton';
import styles from './MovieCollection.css';

class MovieCollection extends Component {

  //
  // Lifecycle

  constructor(props, context) {
    super(props, context);

    this.state = {
      hasPosterError: false,
      isEditImportListModalOpen: false
    };
  }

  onAddImportListPress = (monitored) => {
    if (this.props.collectionList) {
      this.props.onMonitorTogglePress(monitored);
    } else {
      this.props.onMonitorTogglePress(monitored);
      this.setState({ isEditImportListModalOpen: true });
    }
  }

  onEditImportListModalClose = () => {
    this.setState({ isEditImportListModalOpen: false });
  }

  render() {
    const {
      name,
      collectionList,
      isSaving
    } = this.props;

    const monitored = collectionList !== undefined && collectionList.enabled && collectionList.enableAuto;

    return (
      <div>
        <MonitorToggleButton
          className={styles.monitorToggleButton}
          monitored={monitored}
          isSaving={isSaving}
          size={15}
          onPress={this.onAddImportListPress}
        />
        {name}
      </div>
    );
  }
}

MovieCollection.propTypes = {
  tmdbId: PropTypes.number.isRequired,
  name: PropTypes.string.isRequired,
  collectionList: PropTypes.object,
  isSaving: PropTypes.bool.isRequired,
  onMonitorTogglePress: PropTypes.func.isRequired
};

export default MovieCollection;
