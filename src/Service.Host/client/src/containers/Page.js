import { Page } from '@linn-it/linn-form-components-library';
import React from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import config from '../config';

function PageContainer({
    showBreadcrumbs = true,
    children,
    showRequestErrors = false,
    width = 'l',
    title = null,
    defaultAppTitle = 'portal authorization'
}) {
    const location = useLocation();
    const navigate = useNavigate();
    return (
        <Page
            homeUrl={config.appRoot}
            navigate={navigate}
            showBreadcrumbs={showBreadcrumbs}
            location={location}
            width={width}
            showRequestErrors={showRequestErrors}
            title={title}
            defaultAppTitle={defaultAppTitle}
        >
            {children}
        </Page>
    );
}

export default PageContainer;
