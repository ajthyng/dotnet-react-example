import React, { useRef, useReducer, useCallback } from 'react'
import { Form, TextInput, Stack, Button } from 'txstate-react/lib/components'
import { useEvent } from 'txstate-react/lib/hooks'
import { todoAPI } from './TodoAPI'
import dayjs from 'dayjs'

const postReducer = (state, action) => {
  switch (action.type) {
    case 'send':
      return {
        ...state,
        sending: true
      }
    case 'success':
      return {
        ...state,
        sending: false
      }
    case 'failure':
      return {
        ...state,
        error: action.error
      }
    default: return state
  }
}

export const AddTodoForm = props => {
  const formRef = useRef()
  const [state, dispatch] = useReducer(postReducer, {})

  const createdTodo = useEvent('created-todo')
  const onSubmit = useCallback(async ({ form, errors }) => {
    dispatch({ type: 'send' })
    try {
      await todoAPI.post('/create', {
        text: form.text || '',
        due: dayjs().startOf('day').add(Math.floor(Math.random() * 30), 'day')
      }, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      dispatch({ type: 'success' })
      createdTodo()
    } catch (err) {
      dispatch({ type: 'failure', error: err })
    }
  }, [createdTodo])

  const handleSubmit = useCallback(async () => {
    formRef.current.submit()
  }, [])

  return (
    <Form
      ref={formRef}
      onSubmit={onSubmit}
    >
      <Stack spacing={12}>
        <TextInput
          name='text'
          path='text'
          label='Todo Text'
          ariaLabel='text for todo item'
        />
        {state.sending ? <span>Sending Todo...</span> : null}
        <Button label='Add Todo' onClick={handleSubmit} ariaLabel='Add Todo' />
      </Stack>
    </Form>
  )
}
