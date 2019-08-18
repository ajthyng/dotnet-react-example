import React, { useCallback } from 'react'
import { Layout, Stack, Loader } from 'txstate-react/lib/components'
import styled from 'styled-components'
import { AddTodoForm } from './AddTodoForm'
import { todoAPI } from './TodoAPI'
import { TodoView } from './TodoView'

const { Header, Footer, Content } = Layout

const Page = styled(Layout)`
  background-color: white;
  height: 100vh;
`

const TopHeader = styled(Header)`
  background-color: #501214;
  color: white;
  box-shadow: 0 2px 4px #303030;
`

const MaroonFooter = styled(Footer)`
  background-color: #501214;
`

function App () {
  const getTodos = useCallback(async () => {
    const { data } = await todoAPI.get('/list')
    return data
  }, [])

  return (
    <Page
    >
      <TopHeader>
        <h1>Basic Todo Example</h1>
      </TopHeader>
      <Content>
        <Stack spacing={12} style={{ padding: 12 }} horizontal>
          <Stack.Item

          >
            <AddTodoForm />
          </Stack.Item>
          <Stack.Item grow>
            <Loader
              fetch={getTodos}
              View={TodoView}
              ErrorView={props => <div>Error</div>}
            />
          </Stack.Item>
        </Stack>
      </Content>
      <MaroonFooter />
    </Page>
  )
}

export default App
